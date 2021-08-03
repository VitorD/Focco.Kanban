import React,{ useState} from "react";
import Request from "../Utils/HttpRequest";

export default function Login({ history }) {

   const [formLogin, setFormLogin] = useState({ email: "", password: "" });

   const setEmail = (event) => {
      setFormLogin({...formLogin, email: event.target.value});
   }

   const setPassword = (event) => {
      setFormLogin({...formLogin, password: event.target.value});
   }

   const onLogin = async (event) => {
      event.preventDefault();

      const response = await new Request("account/login","POST")
      .setBody(formLogin)
      .send();
            
      if(!response.ok)
      {
         window.alert(response.errorMessage);
         return;
      }

      localStorage.setItem("token",response.data);
      history.push("/");
   }

   const onVoltar = () => {
      history.push("/");
   }
 

   return (

      <div style={{ width: "450px" }}>
         <p>Bem vindo ao <strong>Sunday.com</strong>, melhor sistema para gestao de tasks</p>

         <form onSubmit={onLogin}>
            <label htmlFor="email">Email</label>
            <input id="email" type="email" placeholder="E-mail" value={formLogin.email} onChange={setEmail}></input>
            <label htmlFor="Senha">Senha</label>
            <input id="senha" type="password" placeholder="Senha" value={formLogin.password} onChange={setPassword}></input>

            <button className="btn btn-primary" type="submit">Entrar</button>
            <button className="btn btn-secondary" onClick={onVoltar}>Voltar</button>

         </form>
      </div>

   );

}