// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//class MatchCard extends HTMLElement {
//    connectedCallback() {
//        this.innerHTML = `
//      <div class="match-card">
//        <p>${this.getAttribute("team1")} vs ${this.getAttribute("team2")}</p>
//        <p>${this.getAttribute("time")}</p>
//        <p>Odds: ${this.getAttribute("odds")}</p>
//      </div>
//    `;
//    }
//}

//customElements.define("match-card", MatchCard);

//document.addEventListener("DOMContentLoaded", async () => {
//    const response = await fetch("/api/matches/recent");
//    const matches = await response.json();

//    const carousel = document.getElementById("matches-carousel");
//    matches.forEach(match => {
//        const card = document.createElement("match-card");
//        card.setAttribute("team1", match.team1);
//        card.setAttribute("team2", match.team2);
//        card.setAttribute("time", match.time);
//        card.setAttribute("odds", match.odds);
//        carousel.appendChild(card);
//    });
//});

let currentIndex = 0;
let carouselItems = [];

// Função para obter os dados do backend
async function fetchCarouselData() {
    try {
        const response = await fetch('/api/matches/recent'); // Substitua pela URL do seu backend
        const data = await response.json();

        // Supondo que o backend retorne uma lista de objetos com informações para cada card
        carouselItems = data;
        renderCarousel();  // Renderiza o carrossel após carregar os dados
    } catch (error) {
        console.error('Erro ao obter os dados do carrossel:', error);
    }
}

// Função para renderizar os cards no carrossel
function renderCarousel() {
    const carousel = document.getElementById('carousel');
    carousel.innerHTML = ''; // Limpa o carrossel antes de adicionar novos cards

    // Cria os cards dinamicamente
    carouselItems.forEach(item => {
        const card = document.createElement('div');
        card.classList.add('card');
        card.innerHTML = `
            <img src="${item.image}" alt="${item.title}" />
            <h3>${item.title}</h3>
            <p>${item.description}</p>
        `;
        carousel.appendChild(card);
    });

    // Ajusta o carrossel para mostrar o item correto
    updateCarouselPosition();
}

// Função para mover o carrossel
function moveCarousel(step) {
    currentIndex += step;

    // Impede que o índice ultrapasse os limites
    if (currentIndex < 0) currentIndex = carouselItems.length - 1;
    if (currentIndex >= carouselItems.length) currentIndex = 0;

    updateCarouselPosition();
}

// Função para atualizar a posição do carrossel
function updateCarouselPosition() {
    const carousel = document.getElementById('carousel');
    const width = document.querySelector('.card').offsetWidth + 20; // Calcula a largura de um card, incluindo margem
    carousel.style.transform = `translateX(-${currentIndex * width}px)`;
}

// Inicializa a aplicação ao carregar os dados
window.onload = fetchCarouselData;
