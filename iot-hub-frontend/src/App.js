import React, { useEffect } from "react";
import { ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import { darkTheme } from "./core/config/theme.js";
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { DashboardPage } from "./pages/dashboard/dashboard.page.js";
import { DeviceListPage } from "./pages/device/deviceList.page.js";
import { DeviceDetailsPage } from "./pages/device/deviceDetails.page.js";
import { LoginPage } from "./pages/login/login.page.js";
import { RegisterPage } from "./pages/register/register.page.js";
import { AdminPage } from "./pages/admin/admin.page.js";
import { NotFoundPage } from "./pages/error/NotFound.page.js";
import { ProtectedRoutes } from "./core/router/protectedRoutes.js";
import { useUserAuth } from "./core/hooks/useUserAuth.js";

import axios from 'axios';

export const App = () => {
  const { getUserToken } = useUserAuth();
  const token = getUserToken();
  if (token && token !== '') {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  }

  useEffect(() => {
    document.title = "IoT Hub";
  }, []);

  return (
    <ThemeProvider theme={darkTheme}>
      <CssBaseline />
      <BrowserRouter>
        <Routes>
          <Route element={<ProtectedRoutes expectedRoles={['USER']} />}>
            <Route path="/" element={<DashboardPage />} />
            <Route path="/devices" element={<DeviceListPage />} />
            <Route path="/device:id" element={<DeviceDetailsPage />} />
            <Route element={<ProtectedRoutes expectedRoles={['ADMIN']} />}>
              <Route path="/admin" element={<AdminPage />} />
            </Route>
          </Route>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />

          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
};
