import React from "react";
import Activity from "../Activity";

export default function Pipe({activities, status, onDelete, onUpdate, onActivityDrops}){

    const activitiesList = activities && activities.filter((a) => a.status === status);

    const  onDeleteactivity = (activity) => {

        if(activity)
            onDelete(activity);

    }

    const onUpdateActivity = (activity) => {
        if(onUpdate) onUpdate(activity);

    }

    const onDragActivityOver = (event) => {
        event.preventDefault();
    }
    const onDropActivity = (event) => {
      const activityId =  event.dataTransfer.getData("activityId");

      if(activityId && onActivityDrops) onActivityDrops(activityId);
    }
   
    const title = status === 0 ? "Aguardando": status === 1 ? "Em andamento":"concluido";
    return (
        <div className={`pipe pipe-${status}`} onDragOver={onDragActivityOver} onDrop={onDropActivity}>
            <span className="pipe-title">
                {title} / {activitiesList.length}
            </span>
           {
               activitiesList.map((activity,index) =>
               {
                   return <Activity activity={activity} key={index} onDelete={onDeleteactivity} onUpdate={onUpdateActivity}/>
               })

           }
        </div>
    );


}
