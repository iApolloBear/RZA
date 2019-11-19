using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RZA.Data;
using RZA.Models;

namespace RZA.Controllers
{
    public class WordsController : Controller
    {
        private readonly WordContext _context;

        public WordsController(WordContext context)
        {
            _context = context;
        }

        // GET: Words
        public async Task<IActionResult> Index()
        {
            return View(await _context.Word.ToListAsync());
        }

        // GET: Words/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.Word
                .FirstOrDefaultAsync(m => m.Id == id);
            if (word == null)
            {
                return NotFound();
            }

            return View(word);
        }

        // GET: Words/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Words/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,content,secret")] Word word)
        {
            int[] Alien = gereate_public(5, 17);
            word.secret = Encrypt(Alien, word.content);

            if (ModelState.IsValid)
            {
                _context.Add(word);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(word);
        }

        // GET: Words/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.Word.FindAsync(id);
            if (word == null)
            {
                return NotFound();
            }
            return View(word);
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,content,secret")] Word word)
        {
            int[] Alien = gereate_public(5, 17);
            word.secret = Encrypt(Alien, word.content);

            if (id != word.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(word);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordExists(word.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(word);
        }

        // GET: Words/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var word = await _context.Word
                .FirstOrDefaultAsync(m => m.Id == id);
            if (word == null)
            {
                return NotFound();
            }

            return View(word);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var word = await _context.Word.FindAsync(id);
            _context.Word.Remove(word);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WordExists(int id)
        {
            return _context.Word.Any(e => e.Id == id);
        }

        private int gcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

        private int multiplicative_inversive(int e, int phi)
        {
            int d = 0;
            int x1 = 0;
            int x2 = 1;
            int y1 = 1;
            int temp_phi = phi;

            while (e > 0)
            {
                int temp1 = temp_phi / e;
                int temp2 = temp_phi - temp1 * e;
                temp_phi = e;
                e = temp2;

                int x = x2 - temp1 * x1;
                int y = d - temp1 * y1;

                x2 = x1;
                x1 = x;
                d = y1;
                y1 = y;
            }

            if (temp_phi == 1)
            {
                return d + phi;
            }

            return d + phi;
        }

        private bool is_prime(int num)
        {
            if (num <= 1) return false;
            if (num == 2) return true;
            if (num % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(num));

            for (int i = 3; i <= boundary; i += 2)
                if (num % i == 0)
                    return false;

            return true;
        }

        private int[] gereate_public(int p, int q)
        {
            Random R = new Random();

            if (!is_prime(p) && is_prime(q))
            {
                Console.WriteLine("Los numeros deben ser primos");
            }
            else if (p == q)
            {
                Console.WriteLine("P y Q no pueden ser iguales");
            }

            int n = p * q;

            int phi = (p - 1) * (q - 1);

            int e = R.Next(1, phi);

            int g = gcd(e, phi);
            while (g != 1)
            {
                e = R.Next(1, phi);
                g = gcd(e, phi);
            }

            int d = multiplicative_inversive(e, phi);
            int[] pub = { e, n };
            return pub;
        }

        private String Encrypt(int[] pk, String Text)
        {
            Double a = pk[0];
            Double b = pk[1];
            String Encriptada = "";
            foreach (char c in Text)
            {
                Double x = Math.Pow(c, b);
                Double y = x % b;
                Encriptada += y.ToString();
            }
            return Encriptada;

        }
    }
}
