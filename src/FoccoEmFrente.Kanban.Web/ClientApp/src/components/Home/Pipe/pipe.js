import React from "react";
import Activity from "../Activity";

export default function Pipe({activities, status, onDelete}){

    const activitiesList = activities && activities.filter((a) => a.status === status);

    const  onDeleteactivity = (activity) => {

        if(activity)
            onDelete(activity);

    }
   
    const title = status === 0 ? "Aguardando": status === 1 ? "Em andamento":"concluido";
    return (
        <div className={`pipe pipe-${status}`}>
            <span className="pipe-title">
                {title} / {activitiesList.length}
            </span>
           {
               activitiesList.map((activity,index) =>
               {
                   return <Activity activity={activity} key={index} onDelete={onDeleteactivity}/>
               })

           }
        </div>
    );


}
