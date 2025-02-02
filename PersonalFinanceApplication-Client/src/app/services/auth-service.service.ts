import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { GoogleAuthProvider, User } from 'firebase/auth';
import { catchError, throwError, from, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { gateawayServiceEnvironment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AuthServiceService {
  private gateawayApi = gateawayServiceEnvironment;
  constructor(private afAuth: AngularFireAuth, private http: HttpClient) {}

  getAuthState(): Observable<User | null> {
    return this.afAuth.authState as Observable<User | null>;
  }

  loginWithEmail(email: string, password: string) {
    return this.afAuth.signInWithEmailAndPassword(email, password);
  }

  loginWithGoogle() {
    const provider = new GoogleAuthProvider();
    return this.afAuth.signInWithPopup(provider);
  }

  getToken(): Observable<any> {
    return from(this.afAuth.currentUser).pipe(
      switchMap((user) => {
        if (user) {
          return from(user.getIdToken()).pipe(
            switchMap((idToken) =>
              this.http.post(this.gateawayApi.verifyLoginToken, { idToken })
            )
          );
        }
        throw new Error('User not authenticated');
      })
    );
  }

  logout() {
    return this.afAuth.signOut();
  }
}
