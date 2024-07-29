
        document.addEventListener('DOMContentLoaded', function () {
            const questions = [
                { correctAnswer: '3', name: 'poll-radio-2022' },
                { correctAnswer: '1', name: 'poll-radio-2023' },
                { correctAnswer: '1', name: 'poll-radio-2024' },
            ];

            questions.forEach(question => {
                const options = document.querySelectorAll(`input[name="${question.name}"]`);
                const labels = document.querySelectorAll(`input[name="${question.name}"] ~ .answer-label`);

                options.forEach((option, index) => {
                    option.addEventListener('change', function () {
                        const selectedAnswerIndex = option.id.split('-').pop();
                        const icon = document.createElement('i');
                        icon.classList.add('fas', 'icon');

                        if (selectedAnswerIndex === question.correctAnswer) {
                            // Correct answer selected, disable all options
                            labels[index].classList.add('correct');
                            icon.classList.add('fa-check');
                            labels[index].appendChild(icon);
                            options.forEach(opt => opt.disabled = true);
                        } else {
                            // Incorrect answer selected, disable only this option
                            labels[index].classList.add('incorrect');
                            icon.classList.add('fa-times');
                            labels[index].appendChild(icon);
                            option.disabled = true;
                        }
                    });
                });
            });
        });
  