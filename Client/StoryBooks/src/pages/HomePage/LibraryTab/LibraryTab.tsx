import { 
  IonContent, 
  IonHeader,
   IonPage,
   IonTitle, 
   IonToolbar 
} from '@ionic/react';

import './LibraryTab.css';

const LibraryTab: React.FC = () => {
  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle>Your Library</IonTitle>
        </IonToolbar>
      </IonHeader>
      <IonContent fullscreen>
        <IonHeader collapse="condense">
          <IonToolbar>
            <IonTitle size="large">Your Library</IonTitle>
          </IonToolbar>
        </IonHeader>
      </IonContent>
    </IonPage>
  );
};

export default LibraryTab;