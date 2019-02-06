class BaseInputValueWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-value", props);
    }

    portID = this.props.port.getID();

    setValue = (key, value) => {
        if (value !== "") {
            taskBuilderDataSource.getInputValue(this.portID).fields[key].value = [value];
            this.forceUpdate();
        }
    }

    setFunctionLocked = (locked) => {
        this.props.port.getParent().setLocked(locked);
    }

    renderField = (key, field) => {
        filledValue = taskBuilderDataSource.getInputValue(this.portID).fields[key].value[0] || '';

        switch (field.type) {
            case "text":
                return (
                    <input
                        type="text"
                        key={key}
                        value={filledValue}
                        onFocus={() => this.setFunctionLocked(true)}
                        onInput={() => this.setFunctionLocked(true)}
                        onBlur={() => this.setFunctionLocked(false)}
                        onChange={e => this.setValue(key, e.target.value)}
                    />
                );
            case "dropdown":
                return (
                    <select
                        key={key}
                        value={filledValue}
                        onChange={e => this.setValue(key, e.target.selectedOptions[0].textContent)}
                    >
                        {field.value.map((value, index) =>
                            (
                                <option
                                    key={index}
                                    value={value}
                                >
                                    {value}
                                </option>
                            )
                        )}
                    </select>
                );
        }
    }

    render() {
        switch (this.props.port.getModel().inputType) {
            case "plain":
                return null;
            case "structureOnly":
            case "filled":
                const fields = this.props.port.getModel().structureModel.fields;

                return (
                    <div {...this.getProps()}>
                        {Object.keys(fields).map(key => this.renderField(key, fields[key]))}
                    </div>
                );
        }
    }
}