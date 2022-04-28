import {
  IonButton,
  IonCard,
  IonCardContent,
  IonCardHeader,
  IonCardTitle,
  IonIcon,
  IonItem,
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
import ValidatedInputComponent from '../ValidatedComponents/ValidatedInputComponent';

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
        <ValidatedInputComponent
          name='FirstName'
          label='First Name'
          labelPosition='floating'
          required={true}
          placeholder='Enter your first name'
          validation={firstNameValidation}
          ref={firstNameRef} />
        <ValidatedInputComponent
          name='LastName'
          label='Last Name'
          labelPosition='floating'
          required={true}
          placeholder='Enter your last name'
          validation={lastNameValidation}
          ref={lastNameRef} />
        <ValidatedInputComponent
          name='Email'
          type='email'
          label='Email'
          labelPosition='floating'
          required={true}
          placeholder='Enter your email address'
          validation={emailValidation}
          ref={emailRef} />
        <ValidatedInputComponent
          type='password'
          name='Password'
          label='Password'
          labelPosition='floating'
          required={true}
          placeholder='Enter your password'
          validation={passwordValidation}
          ref={passwordRef} />
        <ValidatedInputComponent
          type='password'
          name='ConfirmPassword'
          label='Confirm Password'
          labelPosition='floating'
          required={true}
          placeholder='Confirm your password'
          validation={confirmPasswordValidation}
          ref={confirmPasswordRef} />
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