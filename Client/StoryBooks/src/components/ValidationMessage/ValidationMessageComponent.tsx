import React from 'react';
import { ValidationError } from '../../utils/common/validation';

import './ValidationMessageComponent.css';

interface ValidationMessageProperties {
    validation?: ValidationError;
}

const ValidationMessage: React.FC<ValidationMessageProperties> = (props) => {
    return (
        <div className='validation-message'>
            {!!props.validation && props.validation.errors.length > 0 && (
                <div className='validation-message-content'>
                    {props.validation.errors.join(', ')}
                </div>
            )}
        </div>
    );
};

export default ValidationMessage;