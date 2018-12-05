class PlainComponent extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            diagram: "initial",
            serialized: null
        };

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick(e) {
        e.preventDefault();
    }

    render() {
        return (
            <div>
                <button onClick={e => this.handleClick(e)}>
                    Serialize Graph!
				</button>
                <div ref="diagram">{this.state.diagram}</div>
            </div>
        );
    }
}