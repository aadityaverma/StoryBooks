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
import { useRef } from 'react';
import { personAddSharp } from 'ionicons/icons';

import { AccountEndpoint } from '../../utils/constants'
import { sendPost } from '../../utils/common/apiCalls'
import { RegisterUserModel } from './RegisterUserModel';
import { UserDetailsModel } from '../../utils/user/userModels';

import './RegisterComponent.css';

interface RegisterComponentProperties {
  onRegister?: (model: UserDetailsModel) => void;
  onSwitch?: () => void;
}

const RegisterComponent: React.FC<RegisterComponentProperties> = (props) => {
  const firstNameRef = useRef<HTMLIonInputElement>(null);
  const lastNameRef = useRef<HTMLIonInputElement>(null);
  const emailRef = useRef<HTMLIonInputElement>(null);
  const passwordRef = useRef<HTMLIonInputElement>(null);
  const confirmPasswordRef = useRef<HTMLIonInputElement>(null);

  const registerUser = () => {
    var model: RegisterUserModel = {
      firstName: firstNameRef.current!.value?.toLocaleString() ?? '',
      lastName: lastNameRef.current!.value?.toLocaleString() ?? '',
      email: emailRef.current!.value?.toLocaleString() ?? '',
      password: passwordRef.current!.value?.toLocaleString() ?? '',
      confirmPassword: confirmPasswordRef.current!.value?.toLocaleString() ?? '',
    };

    sendPost(AccountEndpoint, model)
      .then((response) => {
          debugger;
          if (!!props.onRegister) {
          }
      })
      .catch(err => console.log(err));
  };

  const switchForm = () => {
    if (props.onSwitch) {
      props.onSwitch();
    }
  };

  return (
    <IonCard>
      <IonCardHeader>
        <IonCardTitle>Create new account</IonCardTitle>
      </IonCardHeader>
      <IonCardContent>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>First Name</IonLabel>
          <IonInput
            auto-complete='false'
            aria-required='true'
            name='firstName'
            required
            placeholder='Enter your first name'
            type='text'
            ref={firstNameRef}></IonInput>
        </IonItem>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>Last Name</IonLabel>
          <IonInput
            auto-complete='false'
            aria-required='true'
            name='lastName'
            required
            placeholder='Enter your last name'
            type='text'
            ref={lastNameRef}></IonInput>
        </IonItem>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>Email</IonLabel>
          <IonInput
            auto-complete='false'
            aria-required='true'
            name='email'
            required
            placeholder='Enter your email address'
            type='email'
            ref={emailRef}></IonInput>
        </IonItem>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>Password</IonLabel>
          <IonInput
            auto-complete='false'
            aria-required='true'
            name='password'
            required
            placeholder='Enter your email address'
            type='password'
            ref={passwordRef}></IonInput>
        </IonItem>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>Confirm Password</IonLabel>
          <IonInput
            auto-complete='false'
            aria-required='true'
            name='confirmPassword'
            required
            placeholder='Enter your email address'
            type='password'
            ref={confirmPasswordRef}></IonInput>
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