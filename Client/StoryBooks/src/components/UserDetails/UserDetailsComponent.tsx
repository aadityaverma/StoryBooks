import {
    IonCard,
    IonCardContent,
    IonCardHeader,
    IonCardTitle,
} from '@ionic/react';

interface LoginComponentProperties {
    onLogout?: () => void;
}

const LoginComponent: React.FC<LoginComponentProperties> = (props) => {

    return (
        <IonCard>
            <IonCardHeader>
                <IonCardTitle></IonCardTitle>
            </IonCardHeader>
            <IonCardContent>

            </IonCardContent>
        </IonCard>
    );
};

export default LoginComponent