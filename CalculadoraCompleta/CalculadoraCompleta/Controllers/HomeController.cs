using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraCompleta.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //inicalizar dados para aparecerem no ecra
            ViewBag.Visor = "0";
            //Variaveis
            Session["operador"] = "";
            //verificar estado do ecra limpo/não limpo para saber se a conta acabou
            Session["limpaVisor"] = true;
            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string visor)
        {
            switch (bt)
            {   //determinar a ação
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    //recuperar resultado da decisão sobre limpeza do visor
                    bool limpaEcra = (bool) Session["limpaVisor"];
                    //processa a escrita do visor
                    if (limpaEcra || visor.Equals("0") )
                    {
                        visor = bt;
                        //marcar o visor para continuar a escrita do operando
                        Session["limpaVisor"] = false;
                    }
                    else

                        visor += bt;
                    break;

                case "+/-":
                    visor = Convert.ToDouble(visor) * -1 + "";
                    break;
                case ",":
                    if (!visor.Contains(","))
                        visor += bt;
                    break;
                case "C":
                    // fazer reset ao valor do operador
                    Session["operador"] = "";
                    // fazer reset ao valor do visor
                    Session["operando"] = "";
                    // indicar que o visor está limpo
                    Session["limpaVisor"] = true;
                    // limpar o visor
                    visor = "0";
                    break;
                case "+":
                case "-":
                case "x":
                case ":":
                    //session = variavel de sessão (viewBag Especial vá)
                    // verifica se já houve um botão carregado
                    string auxOperador = (string)Session["operador"];
                    // se já foi carregado
                    //esta condição verifica se o auxOperador é diferente de vazio
                    //se nunca carregarmos no botão a string fica vazia
                    if (!auxOperador.Equals(""))
                    {
                        //realizar calculos
                        double auxOperando1 = Convert.ToDouble(Session["operando"]);
                        double auxOperando2 = Convert.ToDouble(visor);
                        switch (auxOperador)
                        {
                            case "+":
                                visor = auxOperando1 + auxOperando2 + "";
                                break;
                            case "-":
                                visor = auxOperando1 - auxOperando2 + "";
                                break;
                            case "x":
                                visor = auxOperando1 * auxOperando2 + "";
                                break;
                            case ":":
                                visor = auxOperando1 / auxOperando2 + "";
                                break;
                        }
                    }
                    // independentemente de ser a primeira vez que se carrega num operador,
                    // ou não, há que guardar estes valores para memória futura
                    // guardar o valor do operador
                    Session["operador"] = bt;
                    // guardar o valor do visor
                    Session["operando"] = visor;
                    // garantir que esta limpo
                    Session["limpaVisor"] = true;
                    break;
                case "=":
                    // verificar se ja carregaram num operador
                    auxOperador = (string)Session["operador"];
                    // caso tenha sido carregado
                    if (!auxOperador.Equals(""))
                    {
                        // realizam dos calculos
                        double auxOperando1 = Convert.ToDouble(Session["operando"]);
                        double auxOperando2 = Convert.ToDouble(visor);
                        switch (auxOperador)
                        {
                            case "+":
                                visor = auxOperando1 + auxOperando2 + "";
                                break;
                            case "-":
                                visor = auxOperando1 - auxOperando2 + "";
                                break;
                            case "x":
                                visor = auxOperando1 * auxOperando2 + "";
                                break;
                            case ":":
                                visor = auxOperando1 / auxOperando2 + "";
                                break;
                        }
                    }
                    // mesmo que seja a primeira vez carregada temos que guardar o valor porque pode ser preciso
                    // fazer reset ao valor do operador
                    Session["operador"] = "";
                    // guardar valor no visor
                    Session["operando"] = visor;
                    // fazer limpeza
                    Session["limpaVisor"] = true;
                    break;
            }

            ViewBag.Visor = visor;
            return View();
        }

    }
}
