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
import { UserAuthModel } from '../../../utils/user/userModels';

import './ProfileTab.css';

const ProfileTab: React.FC = () => {
  const [loggedUser, setLoggedUser] = useState<boolean>(true);
  const [loginVisible, setLoginVisible] = useState<boolean>(false);
  const [present, dismiss] = useIonToast();

  useEffect(() => {
    isAuthenticated().then((authenticated) => {
      setLoggedUser(authenticated);
      setLoginVisible(!authenticated);
    });
  }, [loggedUser]);

  const handleLogin = (model: UserAuthModel) => {
    setLoggedUser(true);

    present({
      buttons: [{ text: 'close', handler: () => dismiss() }],
      message: 'Login Success!'
    });
  };

  const handleRegistration = (model: string) => {
    setLoginVisible(true);

    present({
      buttons: [{ text: 'close', handler: () => dismiss() }],
      message: 'Register Success!'
    });
  };

  const handleLogOut = () => {
    setLoginVisible(true);
    setLoggedUser(false);

    present({
      buttons: [{ text: 'close', handler: () => dismiss() }],
      message: 'Logout Success!'
    });
  };

  const handleSwitch = () => {
    setLoginVisible(!loginVisible);
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
            {!loginVisible && (
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
