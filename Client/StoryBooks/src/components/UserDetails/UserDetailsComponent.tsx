import {
    IonCard,
    IonCardContent,
    IonCardHeader,
    IonCardTitle,
    IonCol,
    IonRow,
    IonItem,
    IonLabel,
    IonItemDivider,
    IonIcon,
    IonItemGroup
} from '@ionic/react';
import {
    alertCircleOutline,
    book,
    calendarOutline,
    callOutline,
    clipboard,
    cogOutline,
    keyOutline,
    logOutOutline,
    mailOutline,
    mailUnreadOutline,
    maleFemaleOutline,
    sendOutline,
    trashBinOutline
} from 'ionicons/icons';

import { useEffect, useState } from 'react';
import { useUserStore } from '../../utils/user/userStore';
import { sendGet } from '../../utils/common/apiCalls';
import { UserAuthModel, UserDetailsModel } from '../../utils/user/userModels';
import { AccountEndpoint } from '../../utils/constants';

interface UserDetailsComponentProperties {
    onLogout?: () => void;
}

const LoginComponent: React.FC<UserDetailsComponentProperties> = (props) => {
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
        if (!userData || !userData.emailConfirmed) {
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
            <IonCol size='12' sizeMd='10' offsetMd='1' sizeLg='8' offsetLg='2' sizeXl='6' offsetXl='3'  >
                <IonCard>
                    <IonCardHeader>
                        <IonCardTitle>{userData?.firstName} {userData?.lastName}</IonCardTitle>
                    </IonCardHeader>
                    {!!userData && (
                        <IonCardContent>
                            <IonItemGroup id='personal-items'>
                                <IonItemDivider>
                                    <IonLabel>Personal Information</IonLabel>
                                </IonItemDivider>
                                <IonItem>
                                    <IonIcon slot='start' icon={mailOutline} />
                                    <IonLabel>{userData.email}</IonLabel>
                                </IonItem>
                                {!isConfirmed && (
                                    <IonItem>
                                        <IonIcon slot='start' icon={alertCircleOutline} />
                                        <IonLabel color='warning'>Please confirm your email address!</IonLabel>
                                    </IonItem>
                                )}
                                <IonItem>
                                    <IonIcon slot='start' icon={maleFemaleOutline} />
                                    <IonLabel></IonLabel>
                                </IonItem>
                                <IonItem>
                                    <IonIcon slot='start' icon={calendarOutline} />
                                    <IonLabel></IonLabel>
                                </IonItem>
                                <IonItem>
                                    <IonIcon slot='start' icon={callOutline} />
                                    <IonLabel>{userData.phoneNumber}</IonLabel>
                                </IonItem>
                            </IonItemGroup>
                            <IonItemGroup>
                                <IonItemDivider>
                                    <IonLabel>Permissions</IonLabel>
                                </IonItemDivider>
                                {userData.roles.map(role => <IonItem key={role}><IonLabel>{role}</IonLabel></IonItem>)}
                            </IonItemGroup>
                            <IonItemGroup id='actions-items'>
                                <IonItemDivider>
                                    <IonLabel>Actions</IonLabel>
                                </IonItemDivider>
                                <IonItem button>
                                    <IonIcon slot="start" icon={clipboard} />
                                    <IonLabel>Edit Profile</IonLabel>
                                </IonItem>
                                <IonItem button >
                                    <IonIcon slot="start" icon={keyOutline} />
                                    <IonLabel>Change Password</IonLabel>
                                </IonItem>
                                <IonItem button>
                                    <IonIcon slot="start" icon={cogOutline} />
                                    <IonLabel>Preferences</IonLabel>
                                </IonItem>
                                {!isConfirmed && (
                                    <IonItem button>
                                        <IonIcon slot="start" icon={mailUnreadOutline} />
                                        <IonLabel>Re-send confirmation email</IonLabel>
                                        <IonIcon slot="end" icon={sendOutline} />
                                    </IonItem>
                                )}
                                {isConfirmed && !isAuthor && (
                                    <IonItem button>
                                        <IonIcon slot="start" icon={book} />
                                        <IonLabel>Become an Author</IonLabel>
                                    </IonItem>
                                )}
                            </IonItemGroup>
                            <IonItemGroup id='quick-links-items'>
                                <IonItemDivider>
                                    <IonLabel>Quick Links</IonLabel>
                                </IonItemDivider>
                                <IonItem href='/about'>
                                    <IonLabel>About</IonLabel>
                                </IonItem>
                                <IonItem href='/contact'>
                                    <IonLabel>Contact Us</IonLabel>
                                </IonItem>
                                <IonItem href='/faq'>
                                    <IonLabel>FAQ</IonLabel>
                                </IonItem>
                                <IonItem href='/privacy'>
                                    <IonLabel>Privacy Policy</IonLabel>
                                </IonItem>
                            </IonItemGroup>
                            <IonItemGroup id='danger-items'>
                                <IonItem button color='warning' onClick={logout}>
                                    <IonIcon slot="start" icon={logOutOutline} />
                                    <IonLabel>Logout</IonLabel>
                                </IonItem>
                                <IonItemDivider></IonItemDivider>
                                <IonItem button color='danger' onClick={logout}>
                                    <IonIcon slot="start" icon={trashBinOutline} />
                                    <IonLabel>Delete Profile</IonLabel>
                                </IonItem>
                            </IonItemGroup>
                        </IonCardContent>
                    )}
                </IonCard>
            </IonCol>
        </IonRow>
    )
}

export default LoginComponent