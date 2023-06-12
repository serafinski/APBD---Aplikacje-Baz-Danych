/* Kontrolery są odpowiedzialne za: 
- Zapytania przeglądarki
- pobieranie danych modelu
- Widok z template'u który zwróci odpowiedź
*/ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Exercise10.Controllers
{
    public class HelloWorldController : Controller
    {
        //Każda publiczną metode w kontrolerze można wezwać jako HTTP endpoint!

        // 
        // GET: /HelloWorld/
        public IActionResult Index()
        {
            return View();
        }
        
        // 
        // GET: /HelloWorld/Welcome/ 
        
        //przykładowy URL: https://localhost:7042/HelloWorld/Welcome/1?name=Tomasz
        //id - odwołanie do [Parameters] 
        //? - query string -> za nim są parametry metody
        //& - jak mamy więcej niż jeden parametr metody (np. ?cos=xyz&cos2=zyx)
        
        /*public string Welcome(string name, int id = 1)
        {
            //HtmlEncoder chroni przed niechcianym inputem - np. JavaScript'em
            return HtmlEncoder.Default.Encode($"Hello {name}, ID: {id}");
        }*/
        
        //Użycie View Template - pozwala to na dynamiczną odpowiedź (potrzeba przekazać dane z kontrolera do widoku)
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            //Poniższe dane zostaną przekazane do widoku
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            
            return View();
        }
        
    }
}