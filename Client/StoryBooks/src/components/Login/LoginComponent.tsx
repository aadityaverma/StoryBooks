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
import { logIn } from 'ionicons/icons';

import { UserAuthModel } from '../../utils/user/userModels';

import './LoginComponent.css';

interface LoginComponentProperties {
  onLogin?: (model: UserAuthModel) => void;
  onSwitch?: () => void;
}

const LoginComponent: React.FC<LoginComponentProperties> = (props) => {
  const emailRef = useRef<HTMLIonInputElement>(null);
  const passwordRef = useRef<HTMLIonInputElement>(null);

  const loginUser = () => {
    var model: UserAuthModel = {
      id: '', // Comes from the ajax call
      expires: new Date('13/04/2022'),
      token: '' // Comes from the ajax call
    };

    if (props.onLogin) {
      props.onLogin(model);
    }
  };

  const switchForm = () => {
    if (props.onSwitch) {
      props.onSwitch();
    }
  };

  return (
    <IonCard>
      <IonCardHeader>
        <IonCardTitle>Login</IonCardTitle>
      </IonCardHeader>
      <IonCardContent>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>
            Email
          </IonLabel>
          <IonInput
            auto-complete='false'
            aria-required='true'
            name='email'
            required
            placeholder='Enter your email address'
            type='email'
            ref={emailRef}>
          </IonInput>
        </IonItem>
        <IonItem>
          <IonLabel position='floating' aria-required='true'>
            Password
          </IonLabel>
          <IonInput
            auto-complete='false'
            aria-required='true'
            name='password'
            placeholder='Enter your email address'
            required
            type='password'
            ref={passwordRef}>
          </IonInput>
        </IonItem>
        <IonItem className='ion-margin-top'>
          <IonButton size='default' shape='round' color='primary' onClick={loginUser}>
            <IonIcon slot='start' icon={logIn} />
            Login
          </IonButton>
          <IonButton size='default' shape='round' color='default' onClick={switchForm} >
            Register
          </IonButton>
        </IonItem>
      </IonCardContent>
    </IonCard>
  );
};

export default LoginComponent