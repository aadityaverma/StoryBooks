import {
  IonButton,
  IonCard,
  IonCardContent,
  IonCardHeader,
  IonCardTitle,
  IonIcon,
  IonInput,
  IonItem,
  IonLabel,
  IonProgressBar
} from '@ionic/react';
import { useEffect, useRef, useState } from 'react';
import { logIn } from 'ionicons/icons';

import { sendPost } from '../../utils/common/apiCalls';
import { ValidationError, validateIonInput } from '../../utils/common/validation';
import ValidationMessage from '../../components/ValidationMessage/ValidationMessageComponent';

import { useUserStore } from '../../utils/user/userStore';
import { UserAuthModel } from '../../utils/user/userModels';
import { LoginUserModel } from './LoginUserModel';
import { LoginEndpoint } from '../../utils/constants';

import './LoginComponent.css';

interface LoginComponentProperties {
  onLogin?: (model: UserAuthModel) => void;
  onSwitch?: () => void;
}

const LoginComponent: React.FC<LoginComponentProperties> = (props) => {
  const userStore = useUserStore();

  const emailRef = useRef<HTMLIonInputElement>(null);
  const passwordRef = useRef<HTMLIonInputElement>(null);

  const [emailValidation, setEmailValidation] = useState<ValidationError>();
  const [passwordValidation, setPasswordValidation] = useState<ValidationError>();
  const [globalValidation, setGlobalValidation] = useState<ValidationError>();

  const [loading, setLoading] = useState<boolean>(false);

  useEffect(() => {
    return () => { 
    };
  }, [])

  const loginUser = () => {
    if (!validateModel()) {
      return;
    }

    var model: LoginUserModel = {
      email: emailRef.current!.value as string,
      password: passwordRef.current!.value as string
    };

    setLoading(true);
    sendPost<UserAuthModel>(LoginEndpoint, model)
      .then(loginSuccess, loginError);
  }

  const loginSuccess = async (authData: UserAuthModel) => {
    setLoading(false);
    await userStore.setUserAuth(authData);
    if (!!props.onLogin) {
      props.onLogin(authData);
    }
  }

  const loginError = async (errors: ValidationError[]) => {
    setLoading(false);
    if (!!errors && errors.length > 0) {
      for (let i = 0; i < errors.length; i++) {
        applyValidation(errors[i]);
      }
    }
  }

  const switchForm = () => {
    if (props.onSwitch) {
      props.onSwitch();
    }
  }

  const validateModel = (): boolean => {
    const validations: ValidationError[] = [
      validateIonInput(emailRef.current!),
      validateIonInput(passwordRef.current!)
    ];    

    for (let i = 0; i < validations.length; i++) {
      applyValidation(validations[i]);
    }

    return !validations.some((value) => { return value.errors.length > 0 });
  }

  const applyValidation = (validation: ValidationError) => {
    if (validation.key === emailRef.current?.name) {
      setEmailValidation(validation);
    } else if (validation.key === passwordRef.current?.name) {
      setPasswordValidation(validation);
    } else {
      setGlobalValidation(validation);
    }
  }

  return (
    <IonCard className='login-card'>
      <IonCardHeader>
        <IonCardTitle>Login</IonCardTitle>
      </IonCardHeader>
      <IonCardContent>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>
            Email
          </IonLabel>
          <IonInput
            auto-complete='off'
            aria-required='true'
            name='Email'
            required
            placeholder='Enter your email address'
            type='email'
            ref={emailRef}>
          </IonInput>
        </IonItem>
        <ValidationMessage validation={emailValidation} />
        <IonItem>
          <IonLabel position='floating' aria-required='true'>
            Password
          </IonLabel>
          <IonInput
            auto-complete='off'
            aria-required='true'
            name='Password'
            placeholder='Enter your email address'
            required
            type='password'
            ref={passwordRef}>
          </IonInput>
        </IonItem>
        <ValidationMessage validation={passwordValidation} />
        <ValidationMessage validation={globalValidation} />
        <IonItem className='ion-margin-top'>
          <IonButton size='default' shape='round' color='primary' onClick={loginUser} disabled={loading}>
            <IonIcon slot='start' icon={logIn} />
            Login
          </IonButton>
          <IonButton size='default' shape='round' color='default' onClick={switchForm} disabled={loading}>
            Register
          </IonButton>
        </IonItem>
        {loading && (<IonProgressBar className='login-progress-bar' type="indeterminate" />)}
      </IonCardContent>
    </IonCard>
  )
}

export default LoginComponent