import React,{useState} from "react";



export default function Activity({activity,onDelete,onUpdate}){

    const [editing,setEditing] = useState(false);
    const [title,setTitle] = useState(activity.title);

    const onEnterEditMode = () => {
        setEditing(true);

    }

    const setActivityTitle = (value) => {
        setTitle(value);

    }

    const onBlurTitle = () => {
        setEditing(false);
        activity.title = title;
        if(onUpdate)
            onUpdate(activity);

    }

   const onDeleteactivity = () => {
       if(onDelete)
            onDelete(activity);
   }  
    
   const onDragActivity =(event)=>{
       event.dataTransfer.setData("activityId", activity.id);
   } 

    return (
        <div draggable={!editing} className={"activity"} onDragStart={onDragActivity}>
            
            <button className="btn-delete-activity" onClick={onDeleteactivity}>X</button>
            {
            editing ? (<input value={title} autoFocus onBlur={onBlurTitle} onChange={ (event) => setActivityTitle(event.target.value)}/> )
            :
            ( <span onDoubleClick={onEnterEditMode}>
                {activity.title}
            </span>)
            }
            
            
            
        </div>
    )


}