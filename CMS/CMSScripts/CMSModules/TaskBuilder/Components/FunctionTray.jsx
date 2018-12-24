class FunctionTray extends React.Component {
    render() {
        return (
            <div className="task-builder-function-tray">
                {this.props.functions.map(function (f, index) {
                    return <FunctionTrayItem functionModel={f} key={f.Name + index} />;
                })}
            </div>
        );
    }
}