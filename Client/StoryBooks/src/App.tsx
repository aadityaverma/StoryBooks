import { Redirect, Route } from 'react-router-dom';
import {
  IonApp,
  IonIcon,
  IonLabel,
  IonRouterOutlet,
  IonTabBar,
  IonTabButton,
  IonTabs,
  setupIonicReact
} from '@ionic/react';
import { IonReactRouter } from '@ionic/react-router';
import { bagHandleOutline, libraryOutline, personOutline } from 'ionicons/icons';
import ShopTab from './pages/ShopTab/ShopTab';
import LibraryTab from './pages/LibraryTab/LibraryTab';
import ProfileTab from './pages/ProfileTab/ProfileTab';

/* Core CSS required for Ionic components to work properly */
import '@ionic/react/css/core.css';

/* Basic CSS for apps built with Ionic */
import '@ionic/react/css/normalize.css';
import '@ionic/react/css/structure.css';
import '@ionic/react/css/typography.css';

/* Optional CSS utils that can be commented out */
import '@ionic/react/css/padding.css';
import '@ionic/react/css/float-elements.css';
import '@ionic/react/css/text-alignment.css';
import '@ionic/react/css/text-transformation.css';
import '@ionic/react/css/flex-utils.css';
import '@ionic/react/css/display.css';

/* Theme variables */
import './theme/variables.css';

setupIonicReact();

const App: React.FC = () => (
  <IonApp>
    <IonReactRouter>
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
          <Route exact path="/">
            <Redirect to="/shop" />
          </Route>
        </IonRouterOutlet>
        <IonTabBar slot="bottom">
          <IonTabButton tab="shop" href="/shop">
            <IonIcon icon={bagHandleOutline} />
            <IonLabel>Shop</IonLabel>
          </IonTabButton>
          <IonTabButton tab="library" href="/library">
            <IonIcon icon={libraryOutline} />
            <IonLabel>Library</IonLabel>
          </IonTabButton>
          <IonTabButton tab="profile" href="/profile">
            <IonIcon icon={personOutline} />
            <IonLabel>Profile</IonLabel>
          </IonTabButton>
        </IonTabBar>
      </IonTabs>
    </IonReactRouter>
  </IonApp>
);

export default App;
