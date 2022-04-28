import { IonItem, IonIcon, IonLabel } from '@ionic/react';
import { logOutOutline } from 'ionicons/icons';

import { useUserStore } from '../../utils/user/userStore';

interface LogoutProperties {
    onLogout?: () => void;
}

const LogoutComponent: React.FC<LogoutProperties> = (props) => {
    const userStore = useUserStore();

    const logout = async () => {
        await userStore.clearCurrentUser();

        if (!!props.onLogout) {
            props.onLogout();
        }
    }

    return (
        <IonItem button color='warning' onClick={logout}>
            <IonIcon slot="start" icon={logOutOutline} />
            <IonLabel>Logout</IonLabel>
        </IonItem>
    )
}

export default LogoutComponent
