import {
  IonContent,
  IonHeader,
  IonPage,
  IonTitle,
  IonToolbar,
  useIonToast
} from '@ionic/react';

import React, { useState, useEffect } from 'react';
import { useUserStore } from '../../../utils/user/userStore';

import LoginComponent from '../../../components/Login/LoginComponent';
import RegisterComponent from '../../../components/Register/RegisterComponent';
import UserDetailsComponent from '../../../components/UserDetails/UserDetailsComponent';

import './ProfileTab.css';

const ProfileTab: React.FC = () => {
  const userStore = useUserStore();

  const [loggedUser, setLoggedUser] = useState<boolean>(false);
  const [loginVisible, setLoginVisible] = useState<boolean>(false);
  const [registerVisible, setRegisterVisible] = useState<boolean>(false);
  const [showToast, dismissToast] = useIonToast();

  useEffect(() => {
    userStore.isAuthenticated().then((authenticated) => {
      setLoggedUser(authenticated);
      setLoginVisible(!authenticated);
      setRegisterVisible(false);
      console.info('useEffect in ProfileTab');
    })
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [loggedUser])

  const handleLogin = () => {
    setLoggedUser(true);
    
    showToast({
      buttons: [{ text: 'close', handler: () => dismissToast() }],
      message: 'Now you can explore the full experience of Story Books.',
      color: 'success',
      duration: 10000,
      header: 'Logged in successfully!'
    })
  }

  const handleRegistration = (model: string) => {
    setLoginVisible(true);
    setRegisterVisible(false);

    showToast({
      buttons: [{ text: 'close', handler: () => dismissToast() }],
      message: 'Feel free to login into your new account. Also, please check your inbox for email verification.',
      color: 'success',
      duration: 10000,
      header: 'Account created!'
    })
  }

  const handleLogOut = () => {
    setLoggedUser(false);

    showToast({
      buttons: [{ text: 'close', handler: () => dismissToast() }],
      message: 'You cannot read or purchase books if you are not logged in!',
      color: 'warning',
      duration: 10000,
      header: 'Logged out!'
    });
  }

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
  )
}

export default ProfileTab;
