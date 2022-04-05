import {
  IonButton,
  IonCard,
  IonCardContent,
  IonCardHeader,
  IonCardTitle,
  IonIcon,
  IonInput,
  IonItem,
  IonLabel
} from '@ionic/react';
import { useRef, useState } from 'react';
import { personAddSharp } from 'ionicons/icons';

import { sendPost } from '../../utils/common/apiCalls';
import { ValidationError, validateInput } from '../../utils/common/validation';
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

  const registerUser = () => {
    var model: RegisterUserModel = getRegisterModel();
    
    if (!validate(model)) {
      return;
    }

    sendPost(AccountEndpoint, model)
      .then(
        registerSuccess,
        registerError);
  };

  const getRegisterModel = (): RegisterUserModel => {
    var model: RegisterUserModel = {
      firstName: firstNameRef.current!.value?.toLocaleString() ?? '',
      lastName: lastNameRef.current!.value?.toLocaleString() ?? '',
      email: emailRef.current!.value?.toLocaleString() ?? '',
      password: passwordRef.current!.value?.toLocaleString() ?? '',
      confirmPassword: confirmPasswordRef.current!.value?.toLocaleString() ?? ''
    };

    return model;
  }

  const registerSuccess = async (data: Response) => {
    const userId: string = await data.json();
    if (!!props.onRegister) {
      props.onRegister(userId);
    }
  }

  const registerError = async (responseError: Response) => {
    const errors: ValidationError[] = await responseError.json();
    if(!!errors && errors.length > 0) {
      for (let i = 0; i < errors.length; i++) {
        applyError(errors[i]);
      }
    }
  }

  const applyError = (validation: ValidationError) => {
    if(validation.key === firstNameRef.current?.name){
      setFirstNameValidation(validation);
    } else if (validation.key === lastNameRef.current?.name) {
      setLastNameValidation(validation);
    } else if (validation.key === emailRef.current?.name) {
      setEmailValidation(validation);
    } else if (validation.key === passwordRef.current?.name) {
      setPasswordValidation(validation);
    } else if (validation.key === confirmPasswordRef.current?.name) {
      setConfirmPasswordValidation(validation);
    }
  }

  const validate = (model: RegisterUserModel): boolean => {
    setFirstNameValidation(validateInput(firstNameRef.current!));
    setLastNameValidation(validateInput(lastNameRef.current!));
    setEmailValidation(validateInput(emailRef.current!));
    setPasswordValidation(validateInput(passwordRef.current!));
    setConfirmPasswordValidation(validateInput(confirmPasswordRef.current!));

    return isValid();
  }

  const isValid = (): boolean => {
    return (!firstNameValidation || firstNameValidation.errors.length === 0) &&
      (!lastNameValidation || lastNameValidation.errors.length === 0) &&
      (!emailValidation || emailValidation!.errors.length === 0) && 
      (!passwordValidation || passwordValidation!.errors.length === 0) &&
      (!confirmPasswordValidation || confirmPasswordValidation!.errors.length) === 0;
  }

  const switchForm = () => {
    if (props.onSwitch) {
      props.onSwitch();
    }
  }

  const validateChange = (event: Event) => {
    const target: HTMLIonInputElement = event.target;
    applyError(validateInput(target));
  }

  return (
    <IonCard>
      <IonCardHeader>
        <IonCardTitle>Create new account</IonCardTitle>
      </IonCardHeader>
      <IonCardContent>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>First Name</IonLabel>
          <IonInput
            auto-complete='off'
            aria-required='true'
            name='firstName'
            required
            placeholder='Enter your first name'
            type='text'
            onIonChange={validateChange}
            ref={firstNameRef} />
            <ValidationMessage validation={firstNameValidation} />
        </IonItem>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>Last Name</IonLabel>
          <IonInput
            auto-complete='off'
            aria-required='true'
            name='lastName'
            required
            placeholder='Enter your last name'
            type='text'
            ref={lastNameRef} />
            <ValidationMessage validation={lastNameValidation} />
        </IonItem>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>Email</IonLabel>
          <IonInput
            auto-complete='off'
            aria-required='true'
            name='email'
            required
            placeholder='Enter your email address'
            type='email'
            ref={emailRef} />
            <ValidationMessage validation={emailValidation} />
        </IonItem>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>Password</IonLabel>
          <IonInput
            auto-complete='off'
            aria-required='true'
            name='password'
            required
            placeholder='Enter your password'
            type='password'
            ref={passwordRef} />
          <ValidationMessage validation={passwordValidation} />
        </IonItem>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>Confirm Password</IonLabel>
          <IonInput
            auto-complete='off'
            aria-required='true'
            name='confirmPassword'
            required
            placeholder='Confirm your password'
            type='password'
            ref={confirmPasswordRef} />
            <ValidationMessage validation={confirmPasswordValidation} />
        </IonItem>
        <IonItem className='ion-margin-top'>
          <IonButton size='default' shape='round' color='primary' onClick={registerUser}>
            <IonIcon slot='start' icon={personAddSharp} />
            Register
          </IonButton>
          <IonButton size='default' shape='round' color='default' onClick={switchForm}>
            Login
          </IonButton>
        </IonItem>
      </IonCardContent>
    </IonCard>
  );
};

export default RegisterComponent