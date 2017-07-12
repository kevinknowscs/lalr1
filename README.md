# LALR(1) Parser Generation Study

A study and sample code for LALR(1) parser generation

Introduction
------------
Call me crazy, but I've always been fascinated by LALR(1) parser generation, ever since I purchased
Aho, Sethi, & Ullman's timeless classic "Compilers: Principles, Techniques, and Tools" - 2nd Edition,
a.k.a, "The Dragon Book" or sometimes called the "Red Dragon Book" to distinquish it from the 1st
Edition ("Green Dragon") or the 3rd Edition, "Purple Dragon".

Why?
----
Of course the UNIX tool yacc and the GNU port of yacc, bison, have existed for decades. Why would
anyone want to study, learn, or for Heaven's sake actually *write* an LALR(1) parser generator when
we have these great tools? Isn't this a solved problem?

Well, I have my own motivations, and since you're reading this, you must have some motivation as well.

First, I find it interesting and challenging. Again, call me crazy, but finally understanding LALR(1)
at a deep level is actually on my "bucket list". I know, I have a very strange bucket list. To give
you a feel for how strange, another item on my bucket list is to understand Andrew Wile's proof of
Fermat's Last Theorem, but I'm afraid I might not get to that one.

Next, although yacc/bison is a great tool, it only generates output for C, C++, and Java languages. So
if your language of choice is not one of those three languages, then you need to start looking for 
other options. There are other options, usually in the form of an open source project, but those projects
sometimes tend to get abandoned, and at some later point in time, someone might fork it or pick up them
maintenance of it. Anyone wanting to do that sort of thing would need to understand the LALR(1) construction
process. My goal is to create the world's best resource for learning the intricacies of LALR(1) parser
construction.

And finally, I have some ideas for advanced projects that would require a specialized packaging of LALR(1)
parsing and the associated LR(1) parser itself. These include:

* A language with a dynamic syntax that could replace the need for code generation. For example, when Microsoft
extended the C# language to include the LINQ syntax, it would be nice if the language could be extended with
new syntax without the compiler designer needing to do it for us.

* A massively parallel, cloud-based compiler that could compile huge projects quicker, and do it incrementally,
so only the things that changed would need to be re-parsed. I got a chuckle out of the HBO comedy series "Silicon Valley".
In the first season of the show the storyline is based around a "middle-out" compression. Get it. We've got "top-down".
We've got "bottom-up". So why not "middle-out"? It sounds completely nuts and maybe someone has already proven
it to be theoretically impossible, but I wonder if there is some kind of "middle-out" approach that could be taken
with compilers to produce blazingly fast, incremental compilation of huge projects. Anyone wishing to explore such
a concept would surely need to understand the basic LALR(1) process.

* A language agnostic equivalent of Microsoft's Roslyn project, which is necessary for text editors and IDEs to
implement IntelliSense functionality. Also, I've had this idea that you could take the output of Roslyn and throw
the whole thing into a graph database, which would be amazing for continuously built developer documentation. But
in order for that to work, one would not want to rebuild the entire graph database on every single build, and that
takes the thought process back to "middle-out" kind of thinking which would allow only the parts of the graph that
changed between builds to be rebuilt.

Contents
--------

Eventually I plan to include some source code for a "toy" parser generator that can be used to learn the generation
process. It wouldn't be intended to compete with bison, but the code would be optimized for learning.

In the meantime, I'm writing a "notebook" that walks through the parsing examples from the Dragon Book. It is
intended as a companion to the book, not an outright replacement (and, of course, the book covers a lot more
subjects than just LALR(1) parsing).

I've also uploaded Frank DeRemer and Thomas Penello's paper on LALR(1), a seminal work on the subject, published
in ACM in 1982. The paper provides the following copyright information: "Permission to copy without fee all or 
part of this material is granted provided that the copies are not made or distributed for direct commercial
advantage ...".

I'm also developing a series of lectures that I will post on YouTube, which will accelerate the learning process.
The YouTube videos will reference this project and its notebooks, thus making this resource a companion to the
YouTube resources.
