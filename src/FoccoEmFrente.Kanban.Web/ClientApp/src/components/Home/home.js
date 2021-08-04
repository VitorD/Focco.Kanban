import React, { useState, useEffect } from "react";
import Pipe from './Pipe';
import './home.css';
import Request from "../Utils/HttpRequest";


export default function Home({ history }) {
    const [activities, setActivities] = useState([]);

    const token = localStorage.getItem("token");
    if (!token) history.push("/login");

    const loadActivities = async () => {

        const response = await new Request("activities","GET")
        .setToken(token)
        .send();
       
        if (!response.ok) {
            window.alert(["nao foi possivel buscar as tarefas",
                response.errorMessage,
            ]);
            return;
        }

        setActivities(response.data);
    };

    const addActivity = async () => {

        const activity = {
            title: "Nova Atividade",
            status:0
        }

        const response = await new Request("activities","POST")
        .setBody(activity)
        .setToken(token)
        .send();

        if (!response.ok) {
            window.alert(["nao foi possivel inserir a tarefa",
                response.errorMessage,
            ]);
            return;
        }

        setActivities([...activities, response.data]);
    }

   const deleteActivity = async (activity) => {
        const response = await new Request(`activities/${activity.id}`,"DELETE")
        .setToken(token)
        .send();

        if (!response.ok) {
            window.alert(["nao foi possivel excluir a tarefa",
                response.errorMessage,
            ]);
            return;
        }

        setActivities(activities.filter(a => a.id !== activity.id));

    }

    const onExit = () => {
        localStorage.removeItem("token");
        history.push("/login");
    };

    const updateActivity = async (activity) => {
        const response = await new Request('activities',"PUT")
        .setToken(token)
        .setBody(activity)
        .send();

        if (!response.ok) {
            window.alert(["nao foi possivel atualizar a tarefa",
                response.errorMessage,
            ]);
            await loadActivities();
            return;
        }

    }

    const updateActivityStatus = async (activityId,status) => {
        const action = status === 0 ? "todo" : status === 1 ? "doing" : "done";

        const response = await new Request(`activities/${activityId}/${action}`,"PUT")
        .setToken(token)
        .send();

        if (!response.ok) {
            window.alert(["nao foi possivel atualizar o status da tarefa",
                response.errorMessage,
            ]);
            await loadActivities();
            return;
        }
    //modo manual
    // activities.find((a) => a.id === activityId).status = status;
    // setActivities([...activities]);
        await loadActivities();

    };




    useEffect(loadActivities, []);


    return (
        <div style={{ width: "800px" }}>
            <p>Bem vindo ao <strong>Sunday.com</strong></p>

            <p>Esse é seu canvas para organizar suas atividades. Crie novas atividades e mantenha elas sempre atualizadas </p>

            <div className="canvas">
                {
                    [0,1,2].map((status,index) => {
                        return(
                            <Pipe key={index}
                             activities={activities} 
                             status={status} 
                             onDelete={deleteActivity}
                             onUpdate={updateActivity} 
                             onActivityDrops={(activityId) => updateActivityStatus(activityId,status)}
                             />
                        );
                    })
                }
              
            </div>
            <button className="btn btn-primary" onClick={addActivity}>Adicionar atividade</button>

            <button className="btn btn-secondary" onClick={onExit} >Sair</button>


        </div>
    )
}
