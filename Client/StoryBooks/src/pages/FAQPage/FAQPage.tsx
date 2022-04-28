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

const FAQPage: React.FC = () => {
  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle>FAQ</IonTitle>
        </IonToolbar>
      </IonHeader>
      <IonContent fullscreen>
        <IonCard>
          <IonCardTitle>Frequently Asked Questions</IonCardTitle>
        </IonCard>
      </IonContent>
    </IonPage>
  )
};

export default FAQPage;

