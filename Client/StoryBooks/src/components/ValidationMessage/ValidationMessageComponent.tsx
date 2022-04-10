import React, { useEffect, useState } from 'react';
import { ValidationError } from '../../utils/common/validation';

import './ValidationMessageComponent.css';

interface ValidationMessageProperties {
    validation?: ValidationError;
}

const ValidationMessage: React.FC<ValidationMessageProperties> = ({ validation }) => {
    const [errors, setErrors] = useState<JSX.Element[]>([]);

    useEffect(() => {
        if (!!validation && !!validation.errors) {
            let idx = 0;
            const errorItems: JSX.Element[] = validation!.errors.map(
                error => <div key={`${validation}-error-${idx++}`}>{error}</div>);

            setErrors(errorItems);
        }

        console.info('useEffect validation message')
    }, [validation, validation?.errors])

    return (
        <div className='validation-message'>
            {!!validation && validation.errors.length > 0 && (
                <div className='validation-message-content'>
                    {errors}
                </div>
            )}
        </div>
    )
}

export default ValidationMessage;