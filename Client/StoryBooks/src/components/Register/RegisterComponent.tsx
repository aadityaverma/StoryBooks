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
import { useRef, useState } from 'react';
import { personAddSharp } from 'ionicons/icons';

import { sendPost } from '../../utils/common/apiCalls';
import { ValidationError, validateIonInput } from '../../utils/common/validation';
import { AccountEndpoint } from '../../utils/constants';
import { RegisterUserModel } from './RegisterUserModel';

import ValidationMessage from '../../components/ValidationMessage/ValidationMessageComponent';

import './RegisterComponent.css';

interface RegisterComponentProperties {
  onRegister?: (userId: string) => void;
  onSwitch?: () => void;
}

const RegisterComponent: React.FC<RegisterComponentProperties> = (props) => {
  const firstNameRef = useRef<HTMLIonInputElement>(null);
  const lastNameRef = useRef<HTMLIonInputElement>(null);
  const emailRef = useRef<HTMLIonInputElement>(null);
  const passwordRef = useRef<HTMLIonInputElement>(null);
  const confirmPasswordRef = useRef<HTMLIonInputElement>(null);

  const [firstNameValidation, setFirstNameValidation] = useState<ValidationError>();
  const [lastNameValidation, setLastNameValidation] = useState<ValidationError>();
  const [emailValidation, setEmailValidation] = useState<ValidationError>();
  const [passwordValidation, setPasswordValidation] = useState<ValidationError>();
  const [confirmPasswordValidation, setConfirmPasswordValidation] = useState<ValidationError>();
  const [globalValidation, setGlobalValidation] = useState<ValidationError>();

  const [loading, setLoading] = useState<boolean>(false);
 
  const registerUser = () => {
    if (!validateModel()) {
      return;
    }

    var model: RegisterUserModel = {
      firstName: firstNameRef.current!.value?.toLocaleString() ?? '',
      lastName: lastNameRef.current!.value?.toLocaleString() ?? '',
      email: emailRef.current!.value?.toLocaleString() ?? '',
      password: passwordRef.current!.value?.toLocaleString() ?? '',
      confirmPassword: confirmPasswordRef.current!.value?.toLocaleString() ?? ''
    };
   
    setLoading(true);
    sendPost<string>(AccountEndpoint, model)
      .then(registerSuccess, registerError);
  }

  const registerSuccess = async (userId: string) => {
    setLoading(false);
    if (!!props.onRegister) {
      props.onRegister(userId);
    }
  }

  const registerError = async (errors: ValidationError[]) => {
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

  const validateChange = (event: Event) => {
    const target: HTMLIonInputElement = event.target as HTMLIonInputElement;
    var validation = validateIonInput(target);
    applyValidation(validation);
  }

  const validateModel = (): boolean => {
    const validations: ValidationError[] = [
      validateIonInput(firstNameRef.current!),
      validateIonInput(lastNameRef.current!),
      validateIonInput(emailRef.current!),
      validateIonInput(passwordRef.current!),
      validateIonInput(confirmPasswordRef.current!)
    ];    

    for (let i = 0; i < validations.length; i++) {
      applyValidation(validations[i]);
    }

    return !validations.some((value) => { return value.errors.length > 0 });
  }
 
  const applyValidation = (validation: ValidationError) => {
    if (validation.key === firstNameRef.current?.name) {
      setFirstNameValidation(validation);
    } else if (validation.key === lastNameRef.current?.name) {
      setLastNameValidation(validation);
    } else if (validation.key === emailRef.current?.name) {
      setEmailValidation(validation);
    } else if (validation.key === passwordRef.current?.name) {
      setPasswordValidation(validation);
    } else if (validation.key === confirmPasswordRef.current?.name) {
      setConfirmPasswordValidation(validation);
    } else {
      setGlobalValidation(validation);
    }
  }

  return (
    <IonCard className='register-card'>
      <IonCardHeader>
        <IonCardTitle>Create new account</IonCardTitle>
      </IonCardHeader>
      <IonCardContent>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>First Name</IonLabel>
          <IonInput
            auto-complete='off'
            aria-required='true'
            name='FirstName'
            required
            placeholder='Enter your first name'
            type='text'
            onIonChange={validateChange}
            ref={firstNameRef} />
        </IonItem>
        <ValidationMessage validation={firstNameValidation} />
        <IonItem>
          <IonLabel position='floating' aria-required='true'>Last Name</IonLabel>
          <IonInput
            auto-complete='off'
            aria-required='true'
            name='LastName'
            required
            placeholder='Enter your last name'
            type='text'
            onIonChange={validateChange}
            ref={lastNameRef} />
        </IonItem>
        <ValidationMessage validation={lastNameValidation} />
        <IonItem>
          <IonLabel position='floating' aria-required='true'>Email</IonLabel>
          <IonInput
            auto-complete='off'
            aria-required='true'
            name='Email'
            required
            placeholder='Enter your email address'
            type='email'
            onIonChange={validateChange}
            ref={emailRef} />
        </IonItem>
        <ValidationMessage validation={emailValidation} />
        <IonItem>
          <IonLabel position='floating' aria-required='true'>Password</IonLabel>
          <IonInput
            auto-complete='off'
            aria-required='true'
            name='Password'
            required
            placeholder='Enter your password'
            type='password'
            onIonChange={validateChange}
            ref={passwordRef} />
        </IonItem>
        <ValidationMessage validation={passwordValidation} />
        <IonItem>
          <IonLabel position='floating' aria-required='true'>Confirm Password</IonLabel>
          <IonInput
            auto-complete='off'
            aria-required='true'
            name='ConfirmPassword'
            required
            placeholder='Confirm your password'
            type='password'
            onIonChange={validateChange}
            ref={confirmPasswordRef} />
        </IonItem>
        <ValidationMessage validation={confirmPasswordValidation} />
        <ValidationMessage validation={globalValidation} />
        <IonItem className='ion-margin-top'>
          <IonButton size='default' shape='round' color='primary' onClick={registerUser} disabled={loading}>
            <IonIcon slot='start' icon={personAddSharp} />
            Register
          </IonButton>
          <IonButton size='default' shape='round' color='default' onClick={switchForm} disabled={loading}>
            Login
          </IonButton>
        </IonItem>
        {loading && (<IonProgressBar className='register-progress-bar' type="indeterminate" />)}
      </IonCardContent>
    </IonCard>
  )
}

export default RegisterComponent