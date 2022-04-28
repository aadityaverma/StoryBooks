import {
  IonPage,
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
  IonCard,
  IonCardTitle
} from '@ionic/react';
import { } from 'ionicons/icons';

const PrivacyPage: React.FC = () => {
  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle>Privacy</IonTitle>
        </IonToolbar>
      </IonHeader>
      <IonContent fullscreen>
        <IonCard>
          <IonCardTitle>Privacy and Terms</IonCardTitle>
        </IonCard>
      </IonContent>
    </IonPage>
  )
};

export default PrivacyPage;

