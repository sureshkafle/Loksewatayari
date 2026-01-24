const questions = [
    {
        q: "What does HTML stand for?",
        o: ["Hyper Text Markup Language", "High Text Machine Language", "Hyperlinks Text Mark Language", "Home Tool Markup Language"],
        a: 0,
        desc: "HTML stands for Hyper Text Markup Language and is used to structure web pages."
    },

    {
        q: "Which language is used for styling web pages?",
        o: ["HTML", "CSS", "Java", "PHP"],
        a: 1,
        desc: "CSS is used in HTML for styling Web Pages"
    },

    {
        q: "Which is a JavaScript framework?",
        o: ["Laravel", "Django", "Angular", "Flask"],
        a: 2,
        desc: "Angular is a Javascript Framework."
    },

    {
        q: "Which tag is used for JavaScript?",
        o: ["<script>", "<js>", "<javascript>", "<code>"],
        a: 0,
        desc: "Script Tag is used for stylying JavaScript."
    },

    {
        q: "Which CSS property changes text color?",
        o: ["font-color", "text-color", "color", "style"],
        a: 2,
        desc: "Text-color is used "
    },

    {
        q: "Which symbol is used for ID selector?",
        o: [".", "#", "*", "@"],
        a: 1,
        desc: "Symbol for ID Selector."
    },

    {
        q: "Which HTML tag creates a link?",
        o: ["<link>", "<a>", "<href>", "<url>"],
        a: 1,
        desc: "a tag is used for creating link in HTML."
    },

    {
        q: "Which is not a programming language?",
        o: ["Python", "HTML", "Java", "C++"], a: 1,
        desc: ""
    },

    {
        q: "Which company developed JavaScript?",
        o: ["Google", "Microsoft", "Netscape", "Oracle"], a: 2,
        desc: ""
    },

    {
        q: "Which CSS unit is relative?",
        o: ["px", "cm", "em", "mm"], a: 2,
        desc: ""
    }
];

let score = 0;
const quiz = document.getElementById("quiz");
const scoreEl = document.getElementById("score");

const letters = ["A", "B", "C", "D"];

questions.forEach((q, qi) => {
    const div = document.createElement("div");
    div.className = "question";

    const title = document.createElement("div");
    title.className = "question-title";
    title.textContent = `${qi + 1}. ${q.q}`;
    div.appendChild(title);

    q.o.forEach((opt, oi) => {
        const option = document.createElement("div");
        option.className = "option";

        const label = document.createElement("div");
        label.className = "option-label";
        label.textContent = letters[oi];

        const text = document.createElement("div");
        text.className = "option-text";
        text.textContent = opt;

        option.appendChild(label);
        option.appendChild(text);

        option.onclick = () => {
            if (div.classList.contains("answered")) return;
            div.classList.add("answered");

            const allOptions = div.querySelectorAll(".option");
            allOptions.forEach(o => o.classList.add("disabled"));

            if (oi === q.a) {
                option.classList.add("correct");
                score++;
            } else {
                option.classList.add("wrong");
                allOptions[q.a].classList.add("correct");
            }

            scoreEl.textContent = score;

            // ✅ SHOW DESCRIPTION
            desc.style.display = "block";
        };

        div.appendChild(option);
    });

    // 🔹 Description element (hidden initially)
    const desc = document.createElement("span");
    desc.className = "answer-desc";
    desc.textContent = q.desc;
    div.appendChild(desc);

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
/* JS for Subcategory tool tip */
document.addEventListener("DOMContentLoaded", function () {
    var tooltipTriggerList = [].slice.call(
        document.querySelectorAll('[data-bs-toggle="tooltip"]')
    );
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
});