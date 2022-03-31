import {
  IonContent,
  IonHeader,
  IonPage,
  IonTitle,
  IonToolbar,
  useIonToast
} from '@ionic/react';

import React, { useState, useEffect } from 'react';
import { isAuthenticated } from '../../../utils/user/user-store';

import LoginComponent from '../../../components/Login/LoginComponent';
import RegisterComponent from '../../../components/Register/RegisterComponent';
import UserDetailsComponent from '../../../components/UserDetails/UserDetailsComponent';
import { UserDetailsModel, UserAuthModel } from '../../../utils/user/userModels';

import './ProfileTab.css';

const ProfileTab: React.FC = () => {
  const [loggedUser, setLoggedUser] = useState<boolean>(false);
  const [loginVisible, setLoginVisible] = useState<boolean>(false);
  const [registerVisible, setRegisterVisible] = useState<boolean>(false);
  const [present, dismiss] = useIonToast();

  useEffect(() => {
    isAuthenticated().then((authenticated) => {
      setLoggedUser(authenticated);
      setLoginVisible(!authenticated);
      setRegisterVisible(false);
    });
  }, []);

  const handleLogin = (model: UserAuthModel) => {
    setLoggedUser(true);

    present({
      buttons: [{ text: 'close', handler: () => dismiss() }],
      message: 'Login Success!'
    });
  };

  const handleRegistration = (model: UserDetailsModel) => {
    setLoginVisible(true);
    setRegisterVisible(false);

    present({
      buttons: [{ text: 'close', handler: () => dismiss() }],
      message: 'Register Success!'
    });
  };

  const handleLogOut = () => {
    setLoginVisible(true);
    setRegisterVisible(false);
    setLoggedUser(false);

    present({
      buttons: [{ text: 'close', handler: () => dismiss() }],
      message: 'Logout Success!'
    });
  };

  const handleSwitch = () => {
    setLoginVisible(!loginVisible);
    setRegisterVisible(!registerVisible);
  }

  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle>Profile</IonTitle>
        </IonToolbar>
      </IonHeader>
      <IonContent fullscreen>
        <IonHeader collapse="condense">
          <IonToolbar>
            <IonTitle size="large">Profile</IonTitle>
          </IonToolbar>
        </IonHeader>
        {!loggedUser && (
          <React.Fragment>
            {loginVisible && (
              <LoginComponent onLogin={handleLogin} onSwitch={handleSwitch} />
            )}
            {registerVisible && (
              <RegisterComponent onRegister={handleRegistration} onSwitch={handleSwitch} />
            )}
          </React.Fragment>
        )}
        {loggedUser && (
          <UserDetailsComponent onLogout={handleLogOut} />
        )}
      </IonContent>
    </IonPage>
  );
};

export default ProfileTab;
