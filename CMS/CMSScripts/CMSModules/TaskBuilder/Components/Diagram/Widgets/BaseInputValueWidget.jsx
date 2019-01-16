const SRD = window["storm-react-diagrams"];

class BaseInputValueWidget extends SRD.BaseWidget {
    constructor(props) {
        super("port-value", props);
    }

    getClassName() {
        return super.getClassName();
    }

    setValue = (e) => {
        if (e.data !== "") {
            this.props.model.setValue(e.data);
        }
    }

    componentDidUpdate() {
        switch (this.props.inputType) {
            case "field":
                console.log(this.refs.field.value);
                this.props.model.setValue(this.refs.field.value);
                break;
            case "dropdown":
                console.log(this.refs.dropdown.value);
                this.props.model.setValue(this.refs.dropdown.value);
        }
    }

    render() {
        let value;
        if (this.props.defaultValue) {
            value = this.props.defaultValue.value;
        }

        switch (this.props.inputType) {
            case "bare":
            case "plain":
                return <div />;
            case "field":
                return (
                    <div {...this.getProps()}>
                        <input
                            ref="field"
                            type="text"
                            defaultValue={value}
                            onFocus={() => this.props.model.getParent().setLocked(true)}
                            onBlur={() => this.props.model.getParent().setLocked(false)}
                            onChange={e => this.setValue(e)}
                        />
                    </div>
                );
            case "dropdown":
                return (
                    <div {...this.getProps()}>
                        <select
                            ref="dropdown"
                            defaultValue={value}
                            onChange={e => this.setValue(e)}
                        >
                            {this.props.valueOptions.map((option, i) =>
                                (
                                    <option key={i}>
                                        {option.value}
                                    </option>
                                )
                            )}
                        </select>
                    </div>
                );
        }
    }
}