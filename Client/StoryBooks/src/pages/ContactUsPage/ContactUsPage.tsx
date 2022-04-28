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

const ContactUsPage: React.FC = () => {
  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle>Contact Us</IonTitle>
        </IonToolbar>
      </IonHeader>
      <IonContent fullscreen>
        <IonCard>
          <IonCardTitle>Contact Story Books Team</IonCardTitle>
        </IonCard>
      </IonContent>
    </IonPage>
  )
};

export default ContactUsPage;

