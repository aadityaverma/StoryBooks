import { IonAlert, IonItem, IonIcon, IonLabel, useIonToast } from '@ionic/react';
import React, { useEffect, useState } from 'react';
import { trashBinOutline } from 'ionicons/icons';

import { sendDelete } from '../../utils/common/apiCalls';
import { useUserStore } from '../../utils/user/userStore';
import { AccountEndpoint } from '../../utils/constants';
import { ValidationError } from '../../utils/common/validation';
import { UserAuthModel } from '../../utils/user/userModels';

import './UserDetailsComponent.css';

interface DeleteProfileProperties {
    onDelete?: () => void;
}

const DeleteProfileComponent: React.FC<DeleteProfileProperties> = (props) => {
    const userStore = useUserStore();
    const [showToast, dismissToast] = useIonToast();
    const [opened, setOpened] = useState<boolean>(false);

    useEffect(() => {
        console.log('useEffect delete profile')
        return () => { 
            console.log('useEffect cleanup delete profile')
            //props.onDelete = undefined; 
        };
    }, [props])

    const deleteProfile = async (alertData: { password: string }) => {
        if (!alertData.password || alertData.password.length === 0) {
            showToast({
                buttons: [{ text: 'close', handler: () => dismissToast() }],
                color: 'danger',
                duration: 10000,
                header: 'Please provide password!'
              })
            return;
        }

        const authData: UserAuthModel = await userStore.getAuthData();
        sendDelete(AccountEndpoint, alertData, authData)
            .then(deleteSuccess)
            .catch(deleteError);
    }

    const deleteSuccess = async () => {
        await userStore.clearCurrentUser();

        if (!!props.onDelete) {
            props.onDelete();
        }
    }

    const deleteError = (errors: ValidationError[]) => {
        console.log(errors);
    }

    return (
        <IonItem button color='danger' onClick={() => { setOpened(true) }}>
            <IonIcon slot="start" icon={trashBinOutline} />
            <IonLabel>Delete Profile</IonLabel>
            <IonAlert
                isOpen={opened}
                onDidDismiss={() => setOpened(false)}
                cssClass='delete-profile-alert'
                header={'Delete your profile!'}
                message={'Are you sure that you want to remove all of your data and completely delete your profile?'}
                keyboardClose={true}
                inputs={[
                    {
                        name: 'password',
                        type: 'password',
                        placeholder: 'Password',
                        attributes: [{ required: true }]
                    }
                ]}
                buttons={[
                    {
                        text: 'Cancel',
                        role: 'cancel',
                        cssClass: 'secondary'
                    },
                    {
                        text: 'Delete',
                        cssClass: 'danger',
                        handler: deleteProfile
                    }
                ]} />
        </IonItem>
    )
}

export default DeleteProfileComponent
