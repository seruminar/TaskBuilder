const SRD = window["storm-react-diagrams"];

class BaseInputValueWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-value", props);
    }

    setValue = (e) => {
        if (e.data !== "") {
            this.props.model.setValue(e.data);
        }
    }

    //componentDidUpdate() {
    //    switch (this.props.inputType) {
    //        case "fields":
    //            console.log(this.refs.field.value);
    //            this.props.model.setValue(this.refs.field.value);
    //            break;
    //        case "dropdown":
    //            console.log(this.refs.dropdown.value);
    //            this.props.model.setValue(this.refs.dropdown.value);
    //    }
    //}

    render() {
        let defaultValue;
        if (this.props.defaultFieldsModel) {
            defaultValue = this.props.defaultFieldsModel.fields[0].value;
        }

        switch (this.props.inputType) {
            case "plain":
                return <div />;
            case "fields":
                return (
                    <div {...this.getProps()}>
                        {this.props.fieldsModel.fields.map(field =>
                            (
                                <input
                                    type="text"
                                    key={field.key}
                                    defaultValue={defaultValue || field.value}
                                    onFocus={() => this.props.model.getParent().setLocked(true)}
                                    onBlur={() => this.props.model.getParent().setLocked(false)}
                                    onChange={e => this.setValue(e)}
                                />
                            )
                        )}
                    </div>
                );
            case "dropdown":
                return (
                    <div {...this.getProps()}>
                        <select
                            defaultValue={defaultValue}
                            onChange={e => this.setValue(e)}
                        >
                            {this.props.fieldsModel.fields.map(field =>
                                (
                                    <option
                                        key={field.key}
                                    >
                                        {field.value}
                                    </option>
                                )
                            )}
                        </select>
                    </div>
                );
        }
    }
}