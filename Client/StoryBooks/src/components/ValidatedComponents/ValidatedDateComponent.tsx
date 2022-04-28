import { IonItem, IonLabel, IonInput, IonIcon, IonPopover, IonDatetime } from '@ionic/react';
import { calendarOutline } from 'ionicons/icons';
import { forwardRef, useEffect, useState } from 'react';
import { validateIonInput, ValidationError } from '../../utils/common/validation';
import ValidationMessage from '../ValidationMessage/ValidationMessageComponent';
import { LabelPositions } from './LabelPositions';
import { format, parseISO, parse } from 'date-fns';
import { DateFormat } from '../../utils/constants';

interface ValidatedDateProperties {
    id: string;
    validation?: ValidationError | undefined;
    label?: string | undefined;
    labelPosition?: LabelPositions;
    placeholder?: string | undefined;
    name?: string | undefined;
    required?: boolean | undefined;
    value?: string | null | undefined;
    onChange?: (event: Event) => void | undefined;
}

const ValidatedDateComponent = forwardRef<HTMLIonInputElement, ValidatedDateProperties>((props, ref) => {
    const [value, setValue] = useState<string | null | undefined>(props.value);
    const [userValue, setUserValue] = useState<string | null | undefined>(props.value);
    const [labelPosition, setLabelPosition] = useState<LabelPositions>(props.labelPosition);
    const [validationError, setValidationError] = useState<ValidationError | undefined>(props.validation);

    useEffect(() => {
        setValidationError(props.validation);
    }, [props.validation])

    useEffect(() => {
        setLabelPosition(props.labelPosition ?? 'fixed');
    }, [props.labelPosition])

    useEffect(() => {
        if (!!props.value) {
            setValue(getIsoDate(props.value));
            setUserValue(props.value);
        }
        
        
    }, [props.value])

    const validateChange = async (event: Event) => {
        const target: HTMLIonInputElement = event.target as HTMLIonInputElement;

        const date = target.value as string;
        setUserValue(date);
        
        const validation = validateIonInput(target);
        setValidationError(validation);

        if (!!props.onChange) {
            props.onChange(event);
        }
    }

    const handleDateSelect = (event: CustomEvent) => {
        setValue(event.detail.value);
        setUserValue(getUserDate(event.detail.value));
    };

    const getUserDate = (value: string) => {
        return format(parseISO(value), DateFormat);
    };

    const getIsoDate = (value: string) => {
        return parse(value, DateFormat, new Date()).toISOString()
    }

    return (
        <>
            <IonItem>
                {props.label && (
                    <IonLabel position={labelPosition} aria-required={props.required}>{props.label}</IonLabel>
                )}
                <IonInput
                    id={props.id}
                    auto-complete='off'
                    aria-required={props.required}
                    name={props.name}
                    required={props.required}
                    placeholder={props.placeholder}
                    type='text'
                    onIonChange={validateChange}
                    value={userValue}
                    ref={ref} />
                <IonIcon slot='end' icon={calendarOutline} />
                <IonPopover trigger={props.id}>
                    <IonDatetime
                        value={value}
                        locale='en-GB'
                        presentation='date'
                        onIonChange={handleDateSelect} />
                </IonPopover>
            </IonItem>
            <ValidationMessage validation={validationError} />
        </>

    )
})

export default ValidatedDateComponent