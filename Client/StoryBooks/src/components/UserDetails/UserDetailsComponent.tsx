import {
    IonCard,
    IonCardContent,
    IonCardHeader,
    IonCardTitle,
    IonCol,
    IonRow,
    IonItem,
    IonLabel,
    IonInput,
    IonItemDivider,
    IonButton,
    IonIcon
} from '@ionic/react';
import { useEffect, useState } from 'react';
import { keyOutline, logOutOutline, mailUnreadOutline, pencilOutline } from 'ionicons/icons';

import { useUserStore } from '../../utils/user/userStore';
import { sendGet } from '../../utils/common/apiCalls';
import { UserAuthModel, UserDetailsModel } from '../../utils/user/userModels';
import { AccountEndpoint } from '../../utils/constants';

interface LoginComponentProperties {
    onLogout?: () => void;
}

const LoginComponent: React.FC<LoginComponentProperties> = (props) => {
    const userStore = useUserStore();
    const [userData, setUserData] = useState<UserDetailsModel>();
    const [isAuthor, setIsAuthor] = useState<boolean>(false);
    const [isConfirmed, setIsConfirmed] = useState<boolean>(false);

    useEffect(() => {
        loadUserData();
        console.info('useEffect user details');
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    const loadUserData = async () => {
        var userData: UserDetailsModel = await userStore.getUserDetails();
        if (!userData) {
            const authData: UserAuthModel = await userStore.getAuthData();
            if (authData == null) {
                return;
            }

            userData = await sendGet<UserDetailsModel>(AccountEndpoint, authData)
            await userStore.setUserDetails(userData);
        }

        setIsAuthor(await userStore.isAuthor());
        setIsConfirmed(userData.emailConfirmed);
        setUserData(userData);
    }

    const logout = async () => {
        await userStore.clearCurrentUser();

        if (!!props.onLogout) {
            props.onLogout();
        }
    }

    return (
        <IonRow>
            <IonCol>
                <IonCard>
                    <IonCardHeader>
                        <IonCardTitle>Welcome, {userData?.firstName || 'There'}!</IonCardTitle>
                    </IonCardHeader>
                    {!!userData && (
                        <IonCardContent>
                            {!isConfirmed && (
                                <div>Please confirm your email address</div>
                             )}
                            <IonItem>
                                <IonLabel position="stacked">First name</IonLabel>
                                <IonInput value={userData.firstName}> </IonInput>
                            </IonItem>
                            <IonItem>
                                <IonLabel position="stacked">Last name</IonLabel>
                                <IonInput value={userData.lastName}> </IonInput>
                            </IonItem>
                            <IonItem>
                                <IonLabel position="stacked">Phone number</IonLabel>
                                <IonInput value={userData.phoneNumber} type='tel'> </IonInput>
                            </IonItem>
                            <IonItemDivider>
                                <IonLabel>Read-only</IonLabel>
                            </IonItemDivider>
                            <IonItem>
                                <IonLabel position="stacked">Email</IonLabel>
                                <IonInput value={userData.email} disabled={true}> </IonInput>
                            </IonItem>
                            <IonItem>
                                <IonLabel position="stacked">Roles</IonLabel>
                                <IonInput value={userData.roles.join(', ')} disabled={true}> </IonInput>
                            </IonItem>
                        </IonCardContent>
                    )}
                </IonCard>
            </IonCol>
            <IonCol>
                <IonCard>
                    <IonCardHeader>
                        <IonCardTitle>Profile actions!</IonCardTitle>
                    </IonCardHeader>
                    {!!userData && (
                        <IonCardContent>
                            <IonItem>
                                <IonButton expand='block'>
                                    Change password
                                    <IonIcon slot="end" icon={keyOutline} />
                                </IonButton>
                            </IonItem>
                            {!isConfirmed && (
                                <IonItem>
                                    <IonButton>
                                        Re-send confirmation email
                                        <IonIcon slot="end" icon={mailUnreadOutline} />
                                    </IonButton>
                                </IonItem>
                            )}
                            {isConfirmed && !isAuthor && (
                                <IonItem>
                                    <IonButton expand='block' onClick={logout}>
                                        Become an Author
                                        <IonIcon slot="end" icon={pencilOutline} />
                                    </IonButton>
                                </IonItem>
                            )}
                            <IonItem>
                                <IonButton expand='block' onClick={logout}>
                                    Logout
                                    <IonIcon slot="end" icon={logOutOutline} />
                                </IonButton>
                            </IonItem>
                        </IonCardContent>
                    )}
                </IonCard>
            </IonCol>
        </IonRow>
    )
}

export default LoginComponent