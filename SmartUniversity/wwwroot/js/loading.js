document.addEventListener('DOMContentLoaded', function () {
    const loadingScreen = document.getElementById('loading-screen');
    const mainContent = document.getElementById('main-content');

    setTimeout(function () {
        if (loadingScreen) {
            loadingScreen.classList.add('fade-out');
            setTimeout(() => loadingScreen.style.display = 'none', 500);
        }
        if (mainContent) {
            mainContent.classList.remove('d-none');
        }
    }, 2000); // مدة ظهور شاشة التحميل
});

// إظهار شاشة تحميل أثناء التنقل (اختياري)
function navigateWithLoading(url) {
    const loadingScreen = document.getElementById('loading-screen');
    const mainContent = document.getElementById('main-content');

    if (loadingScreen) {
        loadingScreen.style.display = 'flex';
        loadingScreen.classList.remove('fade-out');
    }

    if (mainContent) {
        mainContent.classList.add('d-none');
    }

    setTimeout(() => {
        window.location.href = url;
    }, 500);
}
