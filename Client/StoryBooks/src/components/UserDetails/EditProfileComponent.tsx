import {
    IonModal,
    IonContent,
    IonIcon,
    IonItem,
    IonLabel,
    IonHeader,
    IonTitle,
    IonToolbar,
    IonSelect,
    IonSelectOption,
    IonItemGroup,
    IonItemDivider,
    IonProgressBar
} from '@ionic/react';
import { clipboard, save, saveOutline } from 'ionicons/icons';
import { useRef, useState } from 'react';

import { validateIonInput, ValidationError } from '../../utils/common/validation';
import { EditUserDetailsModel, Gender, UserDetailsModel } from '../../utils/user/userModels';
import { useUserStore } from '../../utils/user/userStore';
import ValidatedDateComponent from '../ValidatedComponents/ValidatedDateComponent';
import ValidatedInputComponent from '../ValidatedComponents/ValidatedInputComponent';
import ValidationMessage from '../ValidationMessage/ValidationMessageComponent';

interface EditProfileProperties {
    model: UserDetailsModel;
    onUpdate?: () => void;
}

const EditProfileComponent: React.FC<EditProfileProperties> = (props) => {
    const userStore = useUserStore();

    const firstNameRef = useRef<HTMLIonInputElement>(null);
    const lastNameRef = useRef<HTMLIonInputElement>(null);
    const phoneRef = useRef<HTMLIonInputElement>(null);
    const dateOfBirthRef = useRef<HTMLIonInputElement>(null);
    const genderRef = useRef<HTMLIonSelectElement>(null);

    const [loading, setLoading] = useState<boolean>(false);
    const [firstNameValidation, setFirstNameValidation] = useState<ValidationError>();
    const [lastNameValidation, setLastNameValidation] = useState<ValidationError>();
    const [phoneValidation, setPhoneValidation] = useState<ValidationError>();
    const [dateOfBirthValidation, setDateOfBirthValidation] = useState<ValidationError>();
    const [genderValidation, setGenderValidation] = useState<ValidationError>();
    const [globalValidation, setGlobalValidation] = useState<ValidationError>();

    const saveDetails = async () => {
        if (!validateModel()) {
            return;
        }

        const userId = await userStore.getUserDetails

        var model: EditUserDetailsModel = {
            id: '',
            firstName: firstNameRef.current!.value?.toLocaleString() ?? '',
            lastName: lastNameRef.current!.value?.toLocaleString() ?? '',
            phoneNumber: phoneRef.current!.value?.toLocaleString() ?? '',
            dateOfBirth: dateOfBirthRef.current!.value?.toLocaleString() ?? '',
            gender: genderRef.current!.value
        }

        setLoading(true);
    }

    const saveSuccess = () => {
        setLoading(false);
    }

    const saveError = async (errors: ValidationError[]) => {
        setLoading(false);
        if (!!errors && errors.length > 0) {
            for (let i = 0; i < errors.length; i++) {
                applyValidation(errors[i]);
            }
        }
    }

    const validateChange = (event: Event) => {
        const target: HTMLIonInputElement = event.target as HTMLIonInputElement;
        var validation = validateIonInput(target);
        applyValidation(validation);
    }

    const applyValidation = (validation: ValidationError) => {
        if (validation.key === firstNameRef.current?.name) {
            setFirstNameValidation(validation);
        } else if (validation.key === lastNameRef.current?.name) {
            setLastNameValidation(validation);
        } else if (validation.key === phoneRef.current?.name) {
            setPhoneValidation(validation);
        } else if (validation.key === dateOfBirthRef.current?.name) {
            setDateOfBirthValidation(validation);
        } else if (validation.key === genderRef.current?.name) {
            setGenderValidation(validation);
        } else {
            setGlobalValidation(validation);
        }
    }

    const validateModel = (): boolean => {
        const validations: ValidationError[] = [
            validateIonInput(firstNameRef.current!),
            validateIonInput(lastNameRef.current!),
            validateIonInput(phoneRef.current!),
            validateIonInput(dateOfBirthRef.current!)
        ];

        for (let i = 0; i < validations.length; i++) {
            applyValidation(validations[i]);
        }

        return !validations.some((value) => { return value.errors.length > 0 });
    }

    return (
        <IonItem button id='trigger-edit-profile-modal'>
            <IonIcon slot="start" icon={clipboard} />
            <IonLabel>Edit Profile</IonLabel>
            <IonModal trigger='trigger-edit-profile-modal'>
                <IonContent>
                    <IonHeader>
                        <IonToolbar color='primary'>
                            <IonTitle >Edit your details</IonTitle>
                        </IonToolbar>
                    </IonHeader>
                    <IonItemGroup>
                        <ValidatedInputComponent
                            name='FirstName'
                            label='First Name'
                            required={true}
                            placeholder='Enter your first name'
                            value={props.model.firstName}
                            validation={firstNameValidation}
                            ref={firstNameRef} />
                        <ValidatedInputComponent
                            name='LastName'
                            label='Last Name'
                            required={true}
                            placeholder='Enter your last name'
                            value={props.model.lastName}
                            validation={lastNameValidation}
                            ref={lastNameRef} />
                        <ValidatedInputComponent
                            name='PhoneNumber'
                            label='Phone'
                            placeholder='Enter your phone number'
                            value={props.model.phoneNumber}
                            validation={phoneValidation}
                            ref={phoneRef} />
                        <ValidatedDateComponent
                            id='date-of-birth'
                            label='Date of birth'
                            name='DateOfBirth'
                            value={props.model.dateOfBirth}
                            validation={dateOfBirthValidation}
                            ref={dateOfBirthRef} />
                        <IonItem>
                            <IonLabel position='fixed'>Gender</IonLabel>
                            <IonSelect
                                placeholder="Select Your Gender"
                                onIonChange={validateChange}
                                name="Gender"
                                ref={genderRef}
                                value={props.model.gender} >
                                <IonSelectOption value={Gender.Female}>Female</IonSelectOption>
                                <IonSelectOption value={Gender.Male}>Male</IonSelectOption>
                                <IonSelectOption value={Gender.Other}>Other</IonSelectOption>
                            </IonSelect>
                        </IonItem>
                        <ValidationMessage validation={genderValidation} />
                        <ValidationMessage validation={globalValidation} />
                        <IonItemDivider />
                        {loading && (<IonProgressBar className='register-progress-bar' type="indeterminate" />)}
                        <IonItem button color='success' onClick={saveDetails}>
                            <IonLabel>Save</IonLabel>
                        </IonItem>
                    </IonItemGroup>
                </IonContent>
            </IonModal>
        </IonItem>
    )
}

export default EditProfileComponent
