class FunctionTrayItem extends React.Component {
    render() {
        return (
            <div
                draggable className="task-builder-tray-item"
                onDragStart={e => {
                    e.dataTransfer.setData("functionModel", JSON.stringify(this.props.functionModel));
                }}

            >
                <i className="icon-w-products-data-source" />
                <span>{this.props.functionModel.Name}</span>
            </div>
        );
    }
}