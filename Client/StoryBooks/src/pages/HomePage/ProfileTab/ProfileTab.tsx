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
  const [showToast, dismissToast] = useIonToast();

  useEffect(() => {
    isAuthenticated().then((authenticated) => {
      setLoggedUser(authenticated);
      setLoginVisible(!authenticated);
    });
  }, [loggedUser]);

  const handleLogin = (model: UserAuthModel) => {
    setLoggedUser(true);

    showToast({
      buttons: [{ text: 'close', handler: () => dismissToast() }],
      message: 'Now you can explore the full experience of Story Books.',
      color: 'success',
      duration: 10000,
      header: 'Logged in successfully!'
    });
  };

  const handleRegistration = (model: string) => {
    setLoginVisible(true);

    showToast({
      buttons: [{ text: 'close', handler: () => dismissToast() }],
      message: 'Feel free to login into your new account. Also, please check your inbox for email verification.',
      color: 'success',
      duration: 10000,
      header: 'Account created!'
    });
  };

  const handleLogOut = () => {
    setLoginVisible(true);
    setLoggedUser(false);

    showToast({
      buttons: [{ text: 'close', handler: () => dismissToast() }],
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
      <IonContent fullscreen className='profile-tab'>
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
