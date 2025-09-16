// Zaman çizelgesi oluşturma
function createTimeline() {
    const timeSlots = document.querySelector('.time-slots');
    const timelineBody = document.querySelector('.timeline-body');
    const days = ['Pazartesi', 'Salı', 'Çarşamba', 'Perşembe', 'Cuma'];

    // Saat başlıklarını oluştur
    for (let hour = 8; hour <= 17; hour++) {
        const timeSlot = document.createElement('div');
        timeSlot.className = 'time-slot';
        timeSlot.textContent = `${hour.toString().padStart(2, '0')}:00`;
        timeSlots.appendChild(timeSlot);
    }

    // Örnek veri
    const breaks = {
        'Pazartesi': [
            { type: 'tuvalet', start: 9.5, duration: 0.25 },
            { type: 'yemek', start: 12, duration: 1 },
            { type: 'mola', start: 15, duration: 0.25 }
        ],
        'Salı': [
            { type: 'tuvalet', start: 10, duration: 0.25 },
            { type: 'yemek', start: 12, duration: 1 },
            { type: 'mola', start: 14.5, duration: 0.25 }
        ],
        // Diğer günler için örnek veriler eklenebilir
    };

    // Günlük satırları oluştur
    days.forEach(day => {
        const row = document.createElement('div');
        row.className = 'timeline-row';

        const dayLabel = document.createElement('div');
        dayLabel.className = 'day-label';
        dayLabel.textContent = day;

        const timeBlocks = document.createElement('div');
        timeBlocks.className = 'time-blocks';

        // O güne ait molaları ekle
        if (breaks[day]) {
            breaks[day].forEach(break_ => {
                const block = document.createElement('div');
                block.className = `time-block block-${break_.type}`;

                // Pozisyon ve genişlik hesaplama
                const startHour = break_.start - 8; // 8'den başladığı için çıkar
                const width = break_.duration;

                block.style.left = `${(startHour / 9) * 100}%`; // 9 saat toplam aralık (8-17)
                block.style.width = `${(width / 9) * 100}%`;

                timeBlocks.appendChild(block);
            });
        }

        row.appendChild(dayLabel);
        row.appendChild(timeBlocks);
        timelineBody.appendChild(row);
    });
}

// PDF dışa aktarma
function exportToPDF() {
    const { jsPDF } = window.jspdf;
    const doc = new jsPDF();

    // Başlık
    doc.setFont("helvetica", "bold");
    doc.text("Haftalık Mola Raporu", 14, 15);

    // Tablo verileri
    const headers = [["Gün", "Mola Tipi", "Başlangıç", "Süre"]];
    const data = [
        ["Pazartesi", "Tuvalet", "09:30", "15 dk"],
        ["Pazartesi", "Yemek", "12:00", "60 dk"],
        ["Pazartesi", "Kısa Mola", "15:00", "15 dk"],
        ["Salı", "Tuvalet", "10:00", "15 dk"],
        ["Salı", "Yemek", "12:00", "60 dk"],
        ["Salı", "Kısa Mola", "14:30", "15 dk"]
    ];

    doc.autoTable({
        head: headers,
        body: data,
        startY: 25,
        styles: { font: "helvetica", fontSize: 10 }
    });

    doc.save("haftalik-rapor.pdf");
}

// Excel dışa aktarma
function exportToExcel() {
    const data = [
        ["Gün", "Mola Tipi", "Başlangıç", "Süre"],
        ["Pazartesi", "Tuvalet", "09:30", "15 dk"],
        ["Pazartesi", "Yemek", "12:00", "60 dk"],
        ["Pazartesi", "Kısa Mola", "15:00", "15 dk"],
        ["Salı", "Tuvalet", "10:00", "15 dk"],
        ["Salı", "Yemek", "12:00", "60 dk"],
        ["Salı", "Kısa Mola", "14:30", "15 dk"]
    ];

    const ws = XLSX.utils.aoa_to_sheet(data);
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, "Mola Raporu");

    XLSX.writeFile(wb, "haftalik-rapor.xlsx");
}

// Sayfa yüklendiğinde timeline'ı oluştur
document.addEventListener('DOMContentLoaded', createTimeline);