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
            //inicalizar dados para a view
            ViewBag.Visor = 0;
            //Variaveis
            Session["operador"] = "";
            Session["limpaVisor"] = true;
            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string visor)
        {
            switch (bt)
            {
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
                    if (visor.Equals("0") || (bool)Session["limpaVisor"])
                    {
                        visor = bt;
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
                    // já alguém carregou num operador?
                    string auxOperador = (string)Session["operador"];
                    // se sim...
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
                    // marcar o visor para limpeza
                    Session["limpaVisor"] = true;
                    break;
                case "=":
                    // já alguém carregou num operador?
                    auxOperador = (string)Session["operador"];
                    // se sim...
                    if (!auxOperador.Equals(""))
                    {
                        // ...vamos realizar os cálculos
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
                    // fazer reset ao valor do operador
                    Session["operador"] = "";
                    // guardar o valor do visor
                    Session["operando"] = visor;
                    // marcar o visor para limpeza
                    Session["limpaVisor"] = true;
                    break;
            }

            ViewBag.Visor = visor;
            return View();
        }

    }
}
