import { Layout } from "../../core/layout/layout.component";
import { Typography, Container } from '@mui/material';

export const AboutPage = () => {
    return (
        <Layout>
            <Container>
                <Typography variant="h5" mt={2} >
                    O Projekcie IoThub
                </Typography>
                <Typography variant="body1" ml={2} >
                    Witaj na stronie projektu IoThub! Jestem studentem Politechniki Krakowskiej, który poświęcił swój czas i wysiłek na stworzenie tego innowacyjnego systemu zarządzania Internetem Rzeczy (IoT).
                </Typography>

                <Typography variant="h6" mt={1} >
                    Kim Jestem
                </Typography>
                <Typography variant="body1" ml={2} >
                    Jestem studentem Politechniki Krakowskiej, pasjonatem technologii i entuzjastą rozwoju projektów z zakresu Internetu Rzeczy. IoThub to efekt mojej pracy nad projektem edukacyjnym, mającym na celu zdobycie praktycznych umiejętności w obszarze programowania i inżynierii.
                </Typography>

                <Typography variant="h6" mt={1} >
                    IoThub - System Zarządzania IoT
                </Typography>
                <Typography variant="body1" ml={2} >
                    IoThub to zaawansowany system zarządzania Internetem Rzeczy, który powstał w ramach mojego projektu edukacyjnego na Politechnice Krakowskiej. Projekt ten jest moim indywidualnym wysiłkiem i został stworzony wyłącznie w celach edukacyjnych, pozwalając mi rozwijać umiejętności w zakresie programowania, inżynierii i projektowania.
                </Typography>

                <Typography variant="h6" mt={1} >
                    Funkcje
                </Typography>
                <ul >
                    <li>Monitorowanie Urządzeń: IoThub umożliwia śledzenie i monitorowanie urządzeń IoT w czasie rzeczywistym, dostarczając istotnych informacji o ich stanie i działaniu.</li>
                    <li>Zdalna Kontrola: Umożliwiam zdalne sterowanie połączonymi urządzeniami, co pozwala na interakcję i zarządzanie nimi z dowolnego miejsca na świecie.</li>
                    <li>Bezpieczeństwo: Bezpieczeństwo danych i urządzeń jest dla mnie priorytetem. IoThub implementuje zaawansowane mechanizmy ochrony, zapewniające poufność i integralność danych.</li>
                </ul>
                <Typography variant="h6" mt={1} >
                    Dlaczego IoThub?
                </Typography>
                <Typography variant="body1" ml={2} >
                    IoThub został stworzony z myślą o dostarczeniu kompleksowego i elastycznego rozwiązania dla zarządzania Internetem Rzeczy. Projekt ten nie tylko spełnia wymagania edukacyjne, ale również stanowi praktyczny wkład w rozwój technologii IoT.
                </Typography>

                <Typography variant="h6" mt={1} >
                    Kontakt
                </Typography>
                <Typography variant="body1" ml={2} >
                    Jeśli masz pytania, sugestie lub chciałbyś dowiedzieć się więcej o IoThub, skontaktuj się ze mną:
                </Typography>
                <ul>
                    <li>E-mail: kontakt@kontakt.pl</li>
                </ul>
                <Typography variant="body1">

                    Dziękuję za zainteresowanie moim projektem!

                    Autor - MCholewa
                </Typography>
            </Container>
        </Layout>
    );
}