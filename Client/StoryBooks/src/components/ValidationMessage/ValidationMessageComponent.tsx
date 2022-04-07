import React from 'react';
import { ValidationError } from '../../utils/common/validation';

import './ValidationMessageComponent.css';

interface ValidationMessageProperties {
    validation?: ValidationError;
}

const ValidationMessage: React.FC<ValidationMessageProperties> = (props) => {
    let idx = 0;
    const errorItems = props.validation?.errors.map(error => <div id={`${props.validation}-error-${idx++}`}>{error}</div>);

    return (
        <div className='validation-message'>
            {!!props.validation && props.validation.errors.length > 0 && (
                <div className='validation-message-content'>
                    {errorItems}
                </div>
            )}
        </div>
    );
};

export default ValidationMessage;