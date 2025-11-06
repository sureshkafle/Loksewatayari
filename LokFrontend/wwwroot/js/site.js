const hamburger = document.querySelector(".hamburger");
const navItemContainer = document.querySelector(".nav-item-container");

hamburger.addEventListener("click",()=> {
     hamburger.classList.toggle("active");
     navItemContainer.classList.toggle("active");
})

document.querySelectorAll(".nav-item").forEach(n => n.
     addEventListener("click",() => {
          hamburger.classList.remove("active");
          navItemContainer.classList.remove("active");
     }))
