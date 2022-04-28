import { TextFieldTypes } from '@ionic/core';
import { IonItem, IonLabel, IonInput } from '@ionic/react';
import { forwardRef, useEffect, useState } from 'react';
import { validateIonInput, ValidationError } from '../../utils/common/validation';
import ValidationMessage from '../ValidationMessage/ValidationMessageComponent';
import { LabelPositions } from './LabelPositions';

interface ValidatedInputProperties {
    validation?: ValidationError | undefined;
    label?: string | undefined;
    labelPosition?: LabelPositions;
    placeholder?: string | undefined;
    name?: string | undefined;
    required?: boolean | undefined;
    type?: TextFieldTypes | undefined;
    value?: string | number | null | undefined;
    onChange?: (event: Event) => void | undefined;
}

const ValidatedInputComponent = forwardRef<HTMLIonInputElement, ValidatedInputProperties>((props, ref) => {
    const [type, setType] = useState<TextFieldTypes | undefined>(props.type);
    const [value, setValue] = useState<string | number | null | undefined>(props.value);
    const [labelPosition, setLabelPosition] = useState<LabelPositions>(props.labelPosition);
    const [validationError, setValidationError] = useState<ValidationError | undefined>(props.validation);
    
    useEffect(() => {
        setValidationError(props.validation);
    }, [props.validation])

    useEffect(() => {
        setType(props.type ?? 'text');
    }, [props.type])

    useEffect(() => {
        setLabelPosition(props.labelPosition ?? 'fixed');
    }, [props.labelPosition])

    useEffect(() => {
        setValue(props.value);
    }, [props.value])

    const validateChange = async (event: Event) => {
        const target: HTMLIonInputElement = event.target as HTMLIonInputElement;

        var validation = validateIonInput(target);
        setValue(target.value);
        setValidationError(validation);

        if (!!props.onChange) {
            props.onChange(event);
        }
      }

    return (
        <>
            <IonItem>
                {props.label && (
                    <IonLabel position={labelPosition} aria-required={props.required}>{props.label}</IonLabel>
                )}
                <IonInput
                    auto-complete='off'
                    aria-required={props.required}
                    name={props.name}
                    required={props.required}
                    placeholder={props.placeholder}
                    type={type}
                    onIonChange={validateChange}
                    value={value} 
                    ref={ref} />
            </IonItem>
            <ValidationMessage validation={validationError} />
        </>

    )
})

export default ValidatedInputComponent