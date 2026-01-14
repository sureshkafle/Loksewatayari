const questions = [
    {
        q: "What does HTML stand for?",
        o: ["Hyper Text Markup Language", "High Text Machine Language", "Hyperlinks Text Mark Language", "Home Tool Markup Language"], a: 0
    },

    {
        q: "Which language is used for styling web pages?",
        o: ["HTML", "CSS", "Java", "PHP"], a: 1
    },

    {
        q: "Which is a JavaScript framework?",
        o: ["Laravel", "Django", "Angular", "Flask"], a: 2
    },

    {
        q: "Which tag is used for JavaScript?",
        o: ["<script>", "<js>", "<javascript>", "<code>"], a: 0
    },

    {
        q: "Which CSS property changes text color?",
        o: ["font-color", "text-color", "color", "style"], a: 2
    },

    {
        q: "Which symbol is used for ID selector?",
        o: [".", "#", "*", "@"], a: 1
    },

    {
        q: "Which HTML tag creates a link?",
        o: ["<link>", "<a>", "<href>", "<url>"], a: 1
    },

    {
        q: "Which is not a programming language?",
        o: ["Python", "HTML", "Java", "C++"], a: 1
    },

    {
        q: "Which company developed JavaScript?",
        o: ["Google", "Microsoft", "Netscape", "Oracle"], a: 2
    },

    {
        q: "Which CSS unit is relative?",
        o: ["px", "cm", "em", "mm"], a: 2
    }
];

let score = 0;
const quiz = document.getElementById("quiz");
const scoreEl = document.getElementById("score");

questions.forEach((q, qi) => {
    const div = document.createElement("div");
    div.className = "question";

    div.innerHTML = `<h5>${qi + 1}. ${q.q}</h5>`;

    q.o.forEach((opt, oi) => {
        const o = document.createElement("div");
        o.className = "option";
        o.textContent = opt;

        o.onclick = () => {
            if (div.classList.contains("answered")) return;
            div.classList.add("answered");

            const options = div.querySelectorAll(".option");
            options.forEach(x => x.classList.add("disabled"));

            if (oi === q.a) {
                o.classList.add("correct");
                score++;
            } else {
                o.classList.add("wrong");
                options[q.a].classList.add("correct");
            }
            scoreEl.textContent = score;
        };

        div.appendChild(o);
    });

    quiz.appendChild(div);
});

/* TIMER */
let time = 300;
const timer = setInterval(() => {
    let m = Math.floor(time / 60);
    let s = time % 60;

    document.getElementById("timer").textContent =
        `${m.toString().padStart(2, '0')}:${s.toString().padStart(2, '0')}`;

    if (time <= 0) {
        clearInterval(timer);
        document.querySelectorAll(".option")
            .forEach(o => o.classList.add("disabled"));
        alert("Time's up!");
    }
    time--;
}, 1000);


/* JS for main category toggle */

function toggleSub(id) {
    const el = document.getElementById(`sub-${id}`);

    if (el.classList.contains("open")) {
        el.style.maxHeight = "0";
        el.classList.remove("open");
    } else {
        el.classList.add("open");
        el.style.maxHeight = el.scrollHeight + "px";
    }
}