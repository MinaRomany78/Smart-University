
(function(){
  const body = document.body;
  const docEl = document.documentElement;
  const collapseBtn = document.getElementById('collapseSidebarBtn');
  const mobileMenuBtn = document.getElementById('mobileMenuBtn');
  const themeToggle = document.getElementById('themeToggle');

  const savedCollapsed = localStorage.getItem('sua.sidebarCollapsed') === '1';
  const savedTheme = localStorage.getItem('sua.theme') || 'light';
  if(savedCollapsed) body.classList.add('sidebar-collapsed');
  docEl.setAttribute('data-bs-theme', savedTheme);

  collapseBtn?.addEventListener('click', () => {
    body.classList.toggle('sidebar-collapsed');
    localStorage.setItem('sua.sidebarCollapsed', body.classList.contains('sidebar-collapsed') ? '1' : '0');
  });
  mobileMenuBtn?.addEventListener('click', () => {
    body.classList.toggle('sidebar-open');
  });
  themeToggle?.addEventListener('click', () => {
    const next = docEl.getAttribute('data-bs-theme') === 'light' ? 'dark' : 'light';
    docEl.setAttribute('data-bs-theme', next);
    localStorage.setItem('sua.theme', next);
  });
  document.getElementById('mainContent')?.addEventListener('click', ()=> body.classList.remove('sidebar-open'));
})();
