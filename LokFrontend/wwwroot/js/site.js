const hamburger = document.querySelector(".hamburger");
const navItemContainer = document.querySelector(".nav-item-container");
const btn = document.querySelector(".dropdown-btn");
const menu = document.querySelector(".dropdown-content");

// 🟦 Toggle hamburger menu on click
hamburger.addEventListener("click", () => {
  hamburger.classList.toggle("active");
  navItemContainer.classList.toggle("active");
});

// 🟦 Close mobile menu when a nav-item is clicked
// document.querySelectorAll(".nav-item").forEach((n) =>
//   n.addEventListener("click", () => {
//     hamburger.classList.remove("active");
//     navItemContainer.classList.remove("active");
//   })
// );

// 🟦 Handle dropdown click only on mobile (no hover support)
if (window.matchMedia("(hover: none)").matches) {
  btn.addEventListener("click", (e) => {
    e.stopPropagation();
    menu.classList.toggle("show");
  });

  // 🟦 Close dropdown when clicking outside
  document.addEventListener("click", (e) => {
    if (!btn.contains(e.target) && !menu.contains(e.target)) {
      menu.classList.remove("show");
    }
  });
}
