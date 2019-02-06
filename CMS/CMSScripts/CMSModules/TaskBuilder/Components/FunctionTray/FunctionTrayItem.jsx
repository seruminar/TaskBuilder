class FunctionTrayItem extends React.Component {
    render() {
        const { displayName, typeGuid } = this.props.functionModel;

        return (
            <div
                draggable className="task-builder-tray-item"
                onDragStart={e => {
                    e.dataTransfer.setData("typeGuid", typeGuid);
                }}
            >
                <i className="icon-w-products-data-source" />
                <span>{displayName}</span>
            </div>
        );
    }
}