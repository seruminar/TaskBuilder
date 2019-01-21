class FunctionTray extends React.Component {
    render() {
        const collapsedStyle = this.props.functions.length ? null : { flex: 0 };

        return (
            <div className="task-builder-function-tray" style={collapsedStyle}>
                {this.props.functions.map((f, index) =>
                    <FunctionTrayItem functionModel={f} key={index} />
                )}
            </div>
        );
    }
}