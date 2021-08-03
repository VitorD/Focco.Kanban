import React from "react";



export default function Activity({activity,onDelete}){

   const onDeleteactivity = () => {
       if(onDelete)
            onDelete(activity);
   }  
    
    
    return (
        <div className={"activity"}>
            
            <button className="btn-delete-activity" onClick={onDeleteactivity}>X</button>

            <span>
                {activity.title}
            </span>
            
        </div>
    )


}