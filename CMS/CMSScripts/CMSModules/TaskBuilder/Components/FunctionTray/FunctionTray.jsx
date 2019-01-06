class FunctionTray extends React.Component {
    render() {
        const collapsedStyle = this.props.functions.length ? null : { flex: 0 };

        return (
            <div className="task-builder-function-tray" style={collapsedStyle}>
                {this.props.functions.map(function (f, index) {
                    return <FunctionTrayItem functionModel={f} key={f.type + index} />;
                })}
            </div>
        );
    }
}