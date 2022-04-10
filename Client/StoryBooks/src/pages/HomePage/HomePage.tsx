import { Redirect, Route } from 'react-router-dom';
import {
    IonIcon,
    IonLabel,
    IonRouterOutlet,
    IonTabBar,
    IonTabButton,
    IonTabs,
} from '@ionic/react';

import { bagHandleOutline, libraryOutline, personOutline, pencilOutline } from 'ionicons/icons';

import { useState, useEffect } from 'react';

import ShopTab from './ShopTab/ShopTab';
import LibraryTab from './LibraryTab/LibraryTab';
import ProfileTab from './ProfileTab/ProfileTab';

import { useUserStore } from '../../utils/user/userStore';

const HomePage: React.FC = () => {
    const userStore = useUserStore();

    const [ loggedAuthor, setLeggedAuthor ] = useState<boolean>(false);
    const [ loggedUser, setLoggedUser ] = useState<boolean>(false);

    useEffect(() => {
        userStore.isAuthor().then(setLeggedAuthor);
        userStore.isAuthenticated().then(setLoggedUser);
        console.log('useEffect in HomePage');
    }, [userStore]);

    return (
        <IonTabs>
            <IonRouterOutlet>
                <Route exact path="/shop">
                    <ShopTab />
                </Route>
                <Route exact path="/library">
                    <LibraryTab />
                </Route>
                <Route exact path="/profile">
                    <ProfileTab />
                </Route>
                <Route exact path="/book/edit/:id">
                </Route>
                <Route exact path="/book/play/:id">
                </Route>
                <Route exact path="/not-found">
                </Route>
                <Route exact path="/">
                    <Redirect to="/shop" />
                </Route>
            </IonRouterOutlet>
            <IonTabBar slot="bottom">
                <IonTabButton tab="shop" href="/shop">
                    <IonIcon icon={bagHandleOutline} />
                    <IonLabel>Shop</IonLabel>
                </IonTabButton>
                {loggedUser && (
                    <IonTabButton tab="library" href="/library">
                        <IonIcon icon={libraryOutline} />
                        <IonLabel>Library</IonLabel>
                    </IonTabButton>
                )}
                <IonTabButton tab="profile" href="/profile">
                    <IonIcon icon={personOutline} />
                    <IonLabel>Profile</IonLabel>
                </IonTabButton>
                {loggedAuthor && (
                    <IonTabButton tab="profile" href="/workspace">
                        <IonIcon icon={pencilOutline} />
                        <IonLabel>Workspace</IonLabel>
                    </IonTabButton>
                )}
            </IonTabBar>
        </IonTabs>
    )
};

export default HomePage;