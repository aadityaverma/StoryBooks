import { IonPage, IonHeader, IonToolbar, IonTitle, IonContent, IonCard, IonCardTitle } from '@ionic/react';
import {  } from 'ionicons/icons';

const AboutPage: React.FC = () => {
    return (
      <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle>About us</IonTitle>
        </IonToolbar>
      </IonHeader>
      <IonContent fullscreen>
        <IonCard>
          <IonCardTitle>About Story Books</IonCardTitle>
        </IonCard>
      </IonContent>
    </IonPage>
    )
};

export default AboutPage;